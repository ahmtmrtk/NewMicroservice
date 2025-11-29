using Duende.IdentityModel.Client;
using NewMicroservice.Web.Options;
using NewMicroservice.Web.Services;
using System.Net;


namespace NewMicroservice.Web.Pages.Auth.SignUp
{
    public record KeycloakErrorResponse(string ErrorMessage);
    public class SignUpService(IdentityOptions identityOptions, HttpClient httpClient, ILogger<SignUpService> logger)
    {
        public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var address = $"{identityOptions.BaseAddress}/admin/realms/newTenant/users";
            httpClient.SetBearerToken(token);
            var userCreateRequest = CreateUserCreateRequest(model);

            var response = await httpClient.PostAsJsonAsync(address, userCreateRequest);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode != HttpStatusCode.InternalServerError)
                {
                    var keycloakError = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();
                    return ServiceResult.Error(keycloakError?.ErrorMessage ?? "An error occurred while creating the account.");
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                logger.LogError(errorContent);
                return ServiceResult.Error("System error occured.");
            }
            return ServiceResult.Success();
        }
        private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
        {
            return new UserCreateRequest(
                model.UserName,
                true,
                model.FirstName,
                model.LastName,
                model.Email,
                new List<Credential> { new Credential("password", model.Password, false) }
           );
        }
        private async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Address,
                Policy = { RequireHttps = false }
            };
            httpClient.BaseAddress = new Uri(identityOptions.Address);
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync();
            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");
            }
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOptions.Admin.ClientId,
                ClientSecret = identityOptions.Admin.ClientSecret,
            });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Token request failed: {tokenResponse.Error}");

            }
            return tokenResponse.AccessToken!;
        }
    }
}
