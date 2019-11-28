using System;
using System.Threading.Tasks;
using SagaSample.Messaging;
using MassTransit;

namespace SagaSample.Service.Billing
{
    public class OrderProcessedConsumer : IConsumer<IOrderProcessedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderProcessedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"Order id: {orderCommand.OrderId} is being billed.");
        }
    }
}