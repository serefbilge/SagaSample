using System;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SagaSample.Common;
using SagaSample.Messaging;
using SagaSample.OrderUI.Models;

namespace SagaSample.OrderUI.Controllers
{
    public class OrderController : Controller
    {        
        public OrderController(IBusControl bus)
        {
            _bus = bus;
        }

        private readonly IBus _bus;

        // GET: Order
        public ActionResult Index(OrderModel orderModel)
        {
            if (orderModel.OrderId > 0)
                CreateOrder(orderModel);

            return View();
        }

        private void CreateOrder(OrderModel orderModel)
        {
            _bus.Publish<IOrderCommand>(orderModel).Wait();
            //_bus.Publish<IOrderCommand>(new
            //{
            //    orderModel.OrderId,
            //    orderModel.OrderCode
            //}).Wait();
        }
    }
}
