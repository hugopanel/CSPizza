using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Clerk
{
    class ClerkReceive
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var sendChannel = connection.CreateModel())
            using (var receiveChannel = connection.CreateModel())
            {
                // Declare fanout exchange for sending
                sendChannel.ExchangeDeclare(exchange: "fanout_logs", type: ExchangeType.Fanout);

                // Declare topic exchange and queue for receiving
                receiveChannel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
                var result = receiveChannel.QueueDeclare(queue: "topic_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var queueName = result.QueueName;

                // Bind the queue to a specific routing key (e.g., "topic.info")
                receiveChannel.QueueBind(exchange: "topic_logs", queue: queueName, routingKey: "clerk.info");

                Console.WriteLine(" [*] Waiting for topic messages. To exit press CTRL+C");

                // Set up consumer for receiving topic messages
                var consumer = new EventingBasicConsumer(receiveChannel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" Clerk Received Topic Message: {message}");
                };

                receiveChannel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                // Send fanout message
                string fanoutMessage = "Get to it my dears !";
                byte[] fanoutMessageBytes = Encoding.UTF8.GetBytes(fanoutMessage);
                sendChannel.BasicPublish(exchange: "fanout_logs", routingKey: "", basicProperties: null, body: fanoutMessageBytes);

                Console.WriteLine($" Clerk  Sent Fanout Message: {fanoutMessage}");
            }
        }
    }
}
