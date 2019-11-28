using System;
using SagaSample.Common;
using MassTransit.Saga;
//--
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SagaSample.Saga
{    
    class Program
    {
        //private static IBusControl _busControl;
        //private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);

        //static void Main(string[] args)
        //{
        //    Console.Title = "Saga";

        //    //https://gist.github.com/alexeyzimarev/dd857685948baeaea02c11722967e364
        //    AssemblyLoadContext.Default.Unloading += _ => Exit();
        //    Console.CancelKeyPress += (_, __) => Exit();

        //    Serilog.Log.Logger = new LoggerConfiguration()
        //        .Enrich.WithProperty("name", typeof(Program).Assembly.GetName().Name)
        //        .WriteTo.Console()
        //        .CreateLogger();

        //    ConfigureBus();

        //    Serilog.Log.Information("Starting...");
        //    _busControl.Start();
        //    Serilog.Log.Information("Started");

        //    WaitHandle.WaitOne();
        //}

        //private static void ConfigureBus()
        //{
        //    var orderSaga = new OrderSaga();
        //    var repo = new InMemorySagaRepository<OrderSagaState>();

        //    _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        //    {
        //        cfg.PrefetchCount = 50;
        //        cfg.UseConcurrencyLimit(50);
                
        //        var host = cfg.Host(new Uri(SagaSampleConfig.RabbitMQUri), h =>
        //        {
        //            h.Username(SagaSampleConfig.RabbitMQUserName);
        //            h.Password(SagaSampleConfig.RabbitMQPassword);
        //        });

        //        cfg.ReceiveEndpoint(host, SagaSampleConfig.SagaQueueName,
        //            ep =>
        //            {
        //                ep.StateMachineSaga(orderSaga, repo);
        //            });
        //    });
        //}
        //private static void ConfigureBus2()
        //{
        //    var orderSaga = new OrderSaga();
        //    var repo = new InMemorySagaRepository<OrderSagaState>();
                       
        //    _busControl = BusConfigurator.Instance.ConfigureBus((cfg, host) =>
        //        {
        //            cfg.ReceiveEndpoint(host, SagaSampleConfig.SagaQueueName, e =>
        //            {
        //                e.StateMachineSaga(orderSaga, repo);
        //            });

        //            cfg.UseSerilog();
        //        });
        //}
        //private static void Exit()
        //{
        //    Serilog.Log.Information("Exiting...");
        //    _busControl.Stop();
        //}

        static void Main(string[] args)
        {            
            Console.Title = "Saga";

            var logger = new LoggerFactory().CreateLogger<OrderSaga>();
            var orderSaga = new OrderSaga(logger);
            var repo = new InMemorySagaRepository<OrderSagaState>();

            var bus = BusConfigurator.Instance.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, SagaSampleConfig.SagaQueueName, e =>
                {
                    e.StateMachineSaga(orderSaga, repo);
                });
            });

            bus.StartAsync();

            Console.WriteLine("Order saga started..");
            Console.ReadLine();
        }        
    }
}