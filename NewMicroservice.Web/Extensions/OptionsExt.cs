using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NewMicroservice.Web.Options;

namespace NewMicroservice.Web.Extensions
{
    public static class OptionsExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations()
           .ValidateOnStart();

            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);


            services.AddOptions<GatewayOption>().BindConfiguration(nameof(GatewayOption)).ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddOptions<MicroserviceOption>().BindConfiguration(nameof(MicroserviceOption)).ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<GatewayOption>(sp => sp.GetRequiredService<IOptions<GatewayOption>>().Value);
            services.AddSingleton<MicroserviceOption>(sp => sp.GetRequiredService<IOptions<MicroserviceOption>>().Value);
            return services;

        }
    }
}
