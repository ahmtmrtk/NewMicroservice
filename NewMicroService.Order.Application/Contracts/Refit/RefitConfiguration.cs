using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewMicroservice.Shared.Options;
using Microsoft.Extensions.Options;
using NewMicroService.Order.Application.Contracts.Refit.Payment;
using Refit;
using NewMicroservice.Order.Application.Contracts.Refit;

namespace NewMicroService.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHttpClientHandler>();
            services.AddScoped<ClientAuthenticatedHttpClientHandler>();

            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);


            services.AddOptions<ClientSecretOption>().BindConfiguration(nameof(ClientSecretOption))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<ClientSecretOption>(sp =>
                sp.GetRequiredService<IOptions<ClientSecretOption>>().Value);


            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
                {
                    var addressUrlOption = configuration.GetSection(nameof(AddressUrlOptions)).Get<AddressUrlOptions>();


                    configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
                }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();


            return services;
        }
    }
}