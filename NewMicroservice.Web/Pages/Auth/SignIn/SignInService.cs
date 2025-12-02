using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NewMicroservice.Web.Options;
using NewMicroservice.Web.Pages.Auth.SignIn;
using NewMicroservice.Web.Services;
using System.Security.Claims;
namespace UdemyNewMicroservice.Web.Pages.Auth.SignIn
{
    public class SignInService(IHttpContextAccessor httpContextAccessor, TokenService tokenService, IdentityOption identityOption, HttpClient client, ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> AuthenticateAsync(SignInViewModel signInViewModel)
        {
            var tokenResponse = await GetAccessToken(signInViewModel);
            if (tokenResponse.IsError)
            {
                return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }
            var userClaims = tokenService.ExtractClaim(tokenResponse.AccessToken!);
            var authenticationProperties = tokenService.CreateAuthenticationProperties(tokenResponse);

            var claimIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);


            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return ServiceResult.Success();
        }
        


        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");
            }


            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password
            });

            return tokenResponse;
        }
    }
}
