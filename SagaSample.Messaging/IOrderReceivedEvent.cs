﻿using System;

namespace SagaSample.Messaging
{
    public interface IOrderReceivedEvent
    {
        Guid CorrelationId { get; }
        int OrderId { get; }
        string OrderCode { get; }
    }
}