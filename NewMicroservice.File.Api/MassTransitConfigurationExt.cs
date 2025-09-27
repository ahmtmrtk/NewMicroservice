using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewMicroservice.Bus;
using NewMicroservice.Bus.Commands;

namespace NewMicroservice.File.Api
{
    public static class MassTransitConfigurationExt
    {
        
        public static IServiceCollection AddMassTransitExt(this IServiceCollection services, IConfiguration configuration)
        {
            var busOptions = configuration.GetSection(nameof(BusOptions)).Get<BusOptions>()!;



            services.AddMassTransit(configure => 
            {
                configure.AddConsumer<UploadCoursePictureCommandCounsumer>();
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                    {
                        host.Username(busOptions.UserName);
                        host.Password(busOptions.Password);
                    });
                    //cfg.ConfigureEndpoints(context);
                    cfg.ReceiveEndpoint("file-microservice-upload-course-picture-command", e =>
                    {
                        e.ConfigureConsumer<UploadCoursePictureCommandCounsumer>(context);
                    });
                });
            });
            return services;
        }
    }
}