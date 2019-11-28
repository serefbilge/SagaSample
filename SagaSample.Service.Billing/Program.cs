using System;
using SagaSample.Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace SagaSample.Service.Billing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BillingService";

            var bus = BusConfigurator.Instance.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host, SagaSampleConfig.SagaQueueName, e =>
                    {
                        e.Consumer<OrderProcessedConsumer>();
                    });
                });

            bus.StartAsync();

            Console.WriteLine("Listening order processed event..");
            Console.ReadLine();
        }
    }
}
