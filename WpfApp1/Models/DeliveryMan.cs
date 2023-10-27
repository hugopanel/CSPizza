using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using RibbitMQ;
using WpfApp1.View;
using static WpfApp1.App;

namespace WpfApp1.Models
{
    public class DeliveryMan : Worker
    {
        /// <summary>
        /// Number of deliveries made.
        /// </summary>
        public int Deliveries { get; set; } = 0;

        [JsonConstructor]
        public DeliveryMan(int id, string name, int deliveries, string address, int nbOrders)
        {
            Id = id;
            Name = name;
            Deliveries = deliveries;
            Address = address;
            NbOrders = nbOrders;
        }

        public DeliveryMan(string name)
        {
            Id = IdCount++;
            Name = name;
        }

        /// <summary>
        /// Deliver an Order to a Customer.
        /// (The Customer the Order will be delivered to is the owner of the Order).
        /// </summary>
        /// <param name="order">The Order to be delivered.</param>
        [Obsolete]
        public async Task Deliver(Order order)
        {
            // TODO: Supprimer cette fonction (et les tests qui l'utilisent)
        }

        public override void CheckIn()
        {
            RibbitMq.Subscribe(MessageType.DeliveryNewOrder, HandleDeliveryNewOrder);
        }

        public override void CheckOut()
        {
            RibbitMq.Unsubscribe(MessageType.DeliveryNewOrder, HandleDeliveryNewOrder);
        }

        private async Task HandleDeliveryNewOrder(IMessage<MessageType> message)
        {
            // We have a new order to deliver!
            HomeView.DeliveryManPrintText("I have a new order to deliver! Let's go!", this);

            Order order = (Order) message.Content!;

            // We tell the Clerk we're on our way
            RibbitMq.Send(new Message()
            {
                MessageType = MessageType.DeliveryGoing,
                Content = order.Id
            });

            RibbitMq.Unsubscribe(MessageType.DeliveryNewOrder, HandleDeliveryNewOrder);

            // Let's deliver!

            try
            {
                Address? customerAddress = Pizzeria.GPS.Find(a => a.Equals(order.Customer.Address ?? ""));

                if (customerAddress == null)
                {
                    // The address is not in the GPS, we must drop the order
                    HomeView.DeliveryManPrintText("The customer's address is not listed on my map! I can't deliver! :(", this);

                    RibbitMq.Send(new Message()
                    {
                        MessageType = MessageType.DeliveryDropped,
                        Content = order.Id
                    });

                    RibbitMq.Subscribe(MessageType.DeliveryNewOrder, HandleDeliveryNewOrder);
                    HomeView.DeliveryManPrintText("I'm ready to make another delivery!", this);
                    return;
                }

                // Deliver the order to the customer
                await Task.Delay(customerAddress.DeliveryTime);

                HomeView.DeliveryManPrintText("I'm at the customer's house!", this);

                RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.DeliveryArrivedToCustomer,
                    Content = order.Id
                });

                // We take the money from the Customer (gently)...
                HomeView.DeliveryManPrintText("I took the money from the customer. Heading back to the pizzeria!", this);

                // Go back to the pizzeria
                await Task.Delay(customerAddress.DeliveryTime);

                HomeView.DeliveryManPrintText("I am back at the pizzeria.", this);

                RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.DeliveryArrivedAtPizzeria,
                    Content = order.Id
                });

                // We give the money to the Clerk...
                HomeView.DeliveryManPrintText("I gave the money to the clerk!", this);

                // We again
                RibbitMq.Subscribe(MessageType.DeliveryNewOrder, HandleDeliveryNewOrder);
                HomeView.DeliveryManPrintText("I'm ready to make another delivery!", this);

                NbOrders++;
            } 
            catch (NullReferenceException ex)
            {
                HomeView.DeliveryManPrintText("Unable to deliver. The customer hasn't specified an address!", this);
                await Console.Out.WriteLineAsync("Unable to deliver. The customer hasn't specified an address.");
            }
        }
    }
}
