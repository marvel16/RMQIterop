using System;
using System.Runtime.InteropServices;
using System.Text;
using RGiesecke.DllExport;
using RabbitMQ.Client;

namespace RabbitClient
{
    public class Exports
    {
        [DllExport("publishMessageToMQ", CallingConvention.Cdecl)]
        public static void PublishMessageToMq([MarshalAs(UnmanagedType.LPStr)] string message)
        {
            Sender.Publish(message);
        }

        [DllExport("openConnection", CallingConvention.Cdecl)]
        public static void OpenConnection()
        {
            Sender.Init();
        }

        [DllExport("closeConnection", CallingConvention.Cdecl)]
        public static void DisposeConnection()
        {
            Sender.Dispose();
        }
    }



    public class Sender
    {
        private static IConnection connection;
        private static IModel channel;
        private static IBasicProperties chProps;

        public static void Init()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(queue: "MQ",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            chProps = channel.CreateBasicProperties();
            chProps.Persistent = true;
        }

        public static void Dispose()
        {
            channel.Dispose();
            connection.Dispose();
        }

        public static void Publish(string message)
        {
            channel.QueueDeclare(queue: "MQ",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);


            channel.BasicPublish(exchange: "",
                routingKey: "MQ",
                basicProperties: chProps,
                body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }
    }
}



