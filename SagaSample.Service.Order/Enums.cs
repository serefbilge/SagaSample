using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaSample.OrderService
{
    public static class Enums
    {
        public enum ReentrancyFlow
        {
            Ignore,
            ThrowException
        }
    }
}
