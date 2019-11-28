using System;
using System.Threading.Tasks;
using SagaSample.Messaging;
using MassTransit;

namespace SagaSample.OrderService
{
    public class OrderReceivedConsumer : IConsumer<IOrderReceivedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderReceivedEvent> context)
        {
            var orderCommand = context.Message;

            await Console.Out.WriteLineAsync($"Order code: {orderCommand.OrderCode} Order id: {orderCommand.OrderId} is received.");

            //do something..

            await context.Publish<IOrderProcessedEvent>(
                new { CorrelationId = context.Message.CorrelationId, OrderId = orderCommand.OrderId });
        }
    }
}