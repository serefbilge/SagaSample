using System;
using SagaSample.Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace SagaSample.OrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            //var serviceProvider = new ServiceCollection().AddSingleton<IBusConfigurator, BusConfigurator>().BuildServiceProvider(); ;
            //var busConfigurator = serviceProvider.GetService<IBusConfigurator>();

            Console.Title = "OrderService";

            var bus = BusConfigurator.Instance.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, SagaSampleConfig.SagaQueueName, e =>
                    {
                        e.Consumer<OrderReceivedConsumer>();
                    });
                });

            bus.StartAsync();

            Console.WriteLine("Listening order received event..");
            Console.ReadLine();
        }
    }
}
