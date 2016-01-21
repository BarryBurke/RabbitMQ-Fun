using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQ.Send
{
    class Send
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    Console.WriteLine("Enter your message:");

                    //string message = "Hello World!";
                    string message = string.Empty;
                    do
                    {
                        message = Console.ReadLine();
                        
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish("", "hello", null, body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                    while (string.IsNullOrEmpty(message) == false);
                }
            }
        }
    }
}
