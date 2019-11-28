using System;
using System.IO;

namespace SagaSample.Common
{
    //https://stackoverflow.com/questions/46940710/getting-value-from-appsettings-json-in-net-core
    //https://stackoverflow.com/questions/46843367/how-to-setbasepath-in-configurationbuilder-in-core-2-0
    public static class SagaSampleConfig
    {
        //static SagaSampleConfig()
        //{
        //    AppSetting = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json")
        //            .Build();
        //}

        //public static IConfiguration AppSetting { get; }
        public static string RabbitMQUri => "rabbitmq://localhost/";//AppSetting["Constants:RabbitMQUri"];
        public static string RabbitMQUserName => "guest"; //AppSetting["Constants:RabbitMQUserName"];
        public static string RabbitMQPassword => "guest"; //AppSetting["Constants:RabbitMQPassword"];
        public static string SagaQueueName => "sagasample.saga"; //AppSetting["Constants:SagaQueueName"];
    }
}
