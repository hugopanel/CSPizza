using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Delivery
{
    class DeliveryReceive
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                // Create a fanout exchange for broadcasting messages to other classes
                channel.ExchangeDeclare(exchange: "fanout_logs", type: ExchangeType.Fanout);

                // Create a unique queue for this class
                var result = channel.QueueDeclare(queue: "", exclusive: true);
                var queueName = result.QueueName;

                string[] bindingKeys = args.Length > 0 ? args : new string[] { "delivery.info" };

                foreach (var bindingKey in bindingKeys)
                {
                    // Bind the queue to both the topic exchange and the fanout exchange
                    channel.QueueBind(exchange: "topic_logs", queue: queueName, routingKey: bindingKey);
                    channel.QueueBind(exchange: "fanout_logs", queue: queueName, routingKey: "");
                }

                Console.WriteLine(" [*] Waiting for logs. To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    Console.WriteLine($" Clerk {e.RoutingKey}:{message}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
