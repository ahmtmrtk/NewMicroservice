using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Threading.Tasks;
using NewMicroservice.Shared.Services;
using Microsoft.Extensions.Configuration;
using NewMicroservice.Shared.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace NewMicroservice.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
            var identityOption = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {

                options.Authority = identityOption.Address;
                options.Audience = identityOption.Audience;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    RoleClaimType = "roles",
                    NameClaimType = "preferred_username"

                };
            })
    .AddJwtBearer("ClientCredentialSchema", options =>
    {

                options.Authority = identityOption.Address;
                options.Audience = identityOption.Audience;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true,

                };
            });

            services.AddAuthorization(options =>
            {


                options.AddPolicy("Password", policy =>
                {

                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Email);

                });

                options.AddPolicy("ClientCredential", policy =>
                {

                    policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("client_id");
                });



            });
            return services;

        }
    }
}