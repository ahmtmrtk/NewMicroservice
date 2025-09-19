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

namespace NewMicroservice.Shared.Extensions
{
    public static class AuthenticationExt
    {
        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {
            var identityOption = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();
            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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
            services.AddAuthorization();
            
            return services;

        }
    }
}