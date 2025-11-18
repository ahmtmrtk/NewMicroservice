using Microsoft.Extensions.Options;
using NewMicroservice.Web.Options;

namespace NewMicroservice.Web.Extensions
{
    public static class OptionsExt
    {
        public static IServiceCollection AddOptionsExt(this IServiceCollection services)
        {
            services.AddOptions<IdentityOptions>()
                .BindConfiguration(nameof(IdentityOptions))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IdentityOptions>(sp =>
                sp.GetRequiredService<IOptions<IdentityOptions>>().Value);
            return services;


        }
    }
}
