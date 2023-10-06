using System;
using RabbitMQ.Client;

namespace TopicSend
{
    class TopicSend
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                Console.WriteLine("Enter your message:");
                string message = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("Enter the routing key:");
                    string routingKey = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(routingKey))
                    {
                        var body = System.Text.Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "topic_logs", routingKey: routingKey, basicProperties: null, body: body);
                        Console.WriteLine($" Client Sent to '{routingKey}':'{message}'");
                    }
                    else
                    {
                        Console.WriteLine("Routing key cannot be empty. Message not sent.");
                    }
                }
                else
                {
                    Console.WriteLine("Message cannot be empty. Message not sent.");
                }
            }
        }
    }
}

