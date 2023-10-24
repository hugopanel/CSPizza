using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class DeliveryMan : Worker
    {
        /// <summary>
        /// Number of deliveries made.
        /// </summary>
        public int Deliveries { get; set; } = 0;

        /// <summary>
        /// Whether the deliveryman is delivering (true) or idle (false).
        /// </summary>
        public bool IsDelivering { get; private set; } = false;

        [JsonConstructor]
        public DeliveryMan(int id, string name, int deliveries)
        {
            Id = id;
            Name = name;
            Deliveries = deliveries;
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
        public async Task Deliver(Order order)
        {
            // Prevent another delivery from being made while the task is not complete.
            IsDelivering = true;

            try
            {
                Address? customerAddress = Pizzeria.GPS.Find(a => a.Equals(order.Customer.Address ?? ""));

                if (customerAddress == null)
                {
                    await Console.Out.WriteLineAsync("Unable to deliver. The address indicated by the customer isn't available.");
                    throw new Exception("Invalid address");
                }

                // Deliver the order to the customer
                Task.Delay(customerAddress!.DeliveryTime).Wait();

                // TODO: Take the money

                // Go back to the pizzeria
                Task.Delay(customerAddress!.DeliveryTime).Wait();

                // TODO: Give the money to the Clerk
            } catch (NullReferenceException ex)
            {
                await Console.Out.WriteLineAsync("Unable to deliver. The customer hasn't specified an address.");
            }

            IsDelivering = false; // Allow th edeliveryman to make another delivery
        }
    }
}
