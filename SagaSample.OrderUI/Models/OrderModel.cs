using SagaSample.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SagaSample.OrderUI.Models
{
    public class OrderModel : IOrderCommand
    {
        public Guid CorrelationId { get; set; }
        public string OrderCode { get; set; }

        public int OrderId { get; set; }
    }
}
