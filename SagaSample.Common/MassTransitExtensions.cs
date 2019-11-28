using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace SagaSample.Common
{
    public static class MassTransitExtensions
    {
        public static void AddMassTransitWithRabbitMq(this IServiceCollection services)
        {            
            services.AddMassTransit();

            services.AddSingleton(svcProv =>
            {
                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(SagaSampleConfig.RabbitMQUri), hst =>
                    {
                        hst.Username(SagaSampleConfig.RabbitMQUserName);
                        hst.Password(SagaSampleConfig.RabbitMQPassword);
                    });

                    cfg.UseSerilog();
                });
            });
        }

        public static void UseMassTransit(this IApplicationBuilder app)
        {
            // start/stop the bus with the web application
            var appLifetime = (app ?? throw new ArgumentNullException(nameof(app))).ApplicationServices.GetService<IApplicationLifetime>();
            var bus = app.ApplicationServices.GetService<IBusControl>();

            appLifetime.ApplicationStarted.Register(() => bus.Start());
            appLifetime.ApplicationStopped.Register(bus.Stop);
        }
    }
}
