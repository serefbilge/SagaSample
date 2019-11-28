using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace SagaSample.Common
{
    public interface IBusConfigurator
    {
        IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null);
        BusConfigurator UseRetry(IRetryPolicy retryPolicy);
        BusConfigurator UseCircuitBreaker(int tripThreshold, int activeThreshold, int resetInterval);
        BusConfigurator UseRateLimiter(int rateLimit, int interval);
    }
}
