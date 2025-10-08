using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewMicroservice.Shared.Options;
using NewMicroService.Order.Application.Contracts.Refit.Payment;
using Refit;

namespace NewMicroService.Order.Application.Contracts.Refit;

public static class RefitConfiguration
{
    public static void AddRefitConfigurationExt(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddRefitClient<IPaymentService>().ConfigureHttpClient(x =>
        {   
            var addressOption = configuration.GetSection(nameof(AddressUrlOptions)).Get<AddressUrlOptions>();
            x.BaseAddress = new Uri(addressOption!.PaymentUrl);
        });
    }
}