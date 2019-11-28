using System;

namespace SagaSample.Messaging
{
    public interface IOrderProcessedEvent
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
    }
}