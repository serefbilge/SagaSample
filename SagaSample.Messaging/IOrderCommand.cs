using System;

namespace SagaSample.Messaging
{
    public interface IOrderCommand
    {
        Guid CorrelationId { get; set; }
        int OrderId { get; set; }
        string OrderCode { get; set; }
    }
}