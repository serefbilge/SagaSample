using Automatonymous;
using System;
using SagaSample.Messaging;
using SagaSample.Saga.Messages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SagaSample.Saga
{
    public class OrderSaga : MassTransitStateMachine<OrderSagaState>
    {
        private ILogger _logger;

        public State Received { get; set; }
        public State Processed { get; set; }

        public Event<IOrderCommand> OrderCommand { get; set; }
        public Event<IOrderProcessedEvent> OrderProcessed { get; set; }

        static void HandleOrderCommand(BehaviorContext<OrderSagaState, IOrderCommand> context)
        {
            context.Instance.OrderId = context.Data.OrderId;
            context.Instance.OrderCode = context.Data.OrderCode;
        }

        public OrderSaga(ILogger<OrderSaga> logger)
        {
            _logger = logger;

            InstanceState(s => s.CurrentState);

            Event(() => OrderCommand,
                        cec => cec.CorrelateById(selector => selector.Message.CorrelationId));
            //.CorrelateBy(state => state.OrderCode, context => context.Message.OrderCode)
            //          .SelectId(selector => Guid.NewGuid()));

            Event(() => OrderProcessed, 
                        cec => cec.CorrelateById(selector => selector.Message.CorrelationId));

            // Events can arrive out of order, so we want to make sure that all observed events can created
            // the state machine instance
            Initially(When(OrderCommand)
                            .Then(HandleOrderCommand)
                            //.ThenAsync(context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is received.."))
                            .ThenAsync(ctx => LogAsync(ctx.Data.OrderId, "Order Command Sent", ctx.Instance.CurrentState))
                            .TransitionTo(Received)
                            .Publish(context => new OrderReceivedEvent(context.Instance)),
                      When(OrderProcessed)
                          //.ThenAsync(context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is processed.."))
                          .ThenAsync(ctx => LogAsync(ctx.Data.OrderId, "Order Command Sent", ctx.Instance.CurrentState))
                          .Finalize()
                );

            // during any state, we can handle any of the events, to transition or capture previously
            // missed data.
            DuringAny(
                   When(OrderCommand)
                            .Then(HandleOrderCommand)
                            //.ThenAsync(context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is received.."))
                            .ThenAsync(ctx => LogAsync(ctx.Data.OrderId, "Order Command Sent", ctx.Instance.CurrentState))
                            .TransitionTo(Received)
                            .Publish(context => new OrderReceivedEvent(context.Instance)),
                   When(OrderProcessed)
                       //.ThenAsync(context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is processed.."))
                       .ThenAsync(ctx => LogAsync(ctx.Data.OrderId, "Order Command Sent", ctx.Instance.CurrentState))
                       .Finalize()
                );

            During(Received,
                   When(OrderProcessed)
                       //.ThenAsync(context => Console.Out.WriteLineAsync($"{context.Data.OrderId} order id is processed.."))
                       .ThenAsync(ctx => LogAsync(ctx.Data.OrderId, "Order Command Sent", ctx.Instance.CurrentState))
                       .Finalize()
                );

            SetCompletedWhenFinalized();

            Task LogAsync(int orderId, string msg, State state)
            {
                return Task.Run(() => _logger.LogInformation($"{msg}: orderId = {orderId}, CurrentState = {state}"));
            }
        }
    }
}