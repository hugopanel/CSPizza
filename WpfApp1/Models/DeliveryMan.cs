using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                /*
                if (!Pizzeria.GPS.Contains(order.Customer.Address!))
                {
                    await Console.Out.WriteLineAsync("Unable to deliver. The address indicated by the customer isn't available.");
                    throw new Exception("Invalid address");
                }

                // Deliver the order to the customer
                Task.Delay(order.Customer.Address!.DeliveryTime).Wait();

                // TODO: Take the money

                // Go back to the pizzeria
                Task.Delay(order.Customer.Address!.DeliveryTime).Wait();

                NbOrders++;*/

                // TODO: Give the money to the Clerk
            }
            catch (NullReferenceException ex)
            {
                await Console.Out.WriteLineAsync("Unable to deliver. The customer hasn't specified an address.");
            }

            IsDelivering = false; // Allow th edeliveryman to make another delivery
        }
    }
}