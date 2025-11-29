using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NewMicroservice.Web.Services
{
    public class TokenService
    {
        public List<Claim> ExtractClaim(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            return jwtToken.Claims.ToList();
        }

        public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
        {
            var authenticationToken = new List<AuthenticationToken>
            {
                new()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken!
                },
                new()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken!
                },
                new()
                 {
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o")
                }

            };

            AuthenticationProperties authenticationProperties = new()
            {
                IsPersistent = true
            };
            authenticationProperties.StoreTokens(authenticationToken);

            return authenticationProperties;


        }
    }
}
