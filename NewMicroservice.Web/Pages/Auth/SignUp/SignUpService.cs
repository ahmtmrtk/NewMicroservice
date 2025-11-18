using Duende.IdentityModel.Client;
using NewMicroservice.Web.Options;


namespace NewMicroservice.Web.Pages.Auth.SignUp
{
    public class SignUpService(IdentityOptions identityOptions, HttpClient httpClient, ILogger<SignUpService> logger)
    {
        public async Task CreateAccount(SignUpViewModel model)
        {
            var token = await GetClientCredentialTokenAsAdmin();
            var address = $"{identityOptions.Admin.Address}/users";
            httpClient.SetBearerToken(token);
            var userCreateRequest = CreateUserCreateRequest(model);

            var response = await httpClient.PostAsJsonAsync(address, userCreateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                logger.LogError(errorContent);
            }

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
        public async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOptions.Admin.Address,
                Policy = { RequireHttps = false }
            };
            httpClient.BaseAddress = new Uri(identityOptions.Admin.Address);
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
