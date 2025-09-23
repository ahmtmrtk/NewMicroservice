using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NewMicroservice.Bus
{
    public static class MassTransitConfigurationExt
    {
        
        public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOptions)).Get<BusOptions>()!;



            services.AddMassTransit(configure => 
            {
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                });
            });
            return services;
        }
    }
}