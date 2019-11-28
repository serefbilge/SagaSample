using Automatonymous;
using System;

namespace SagaSample.Saga
{
    public class OrderSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public State CurrentState { get; set; }
        public DateTime? StartTime { get; set; }
        public string FaultSummary { get; set; }

        public int OrderId { get; set; }
        public string OrderCode { get; set; }
    }
}