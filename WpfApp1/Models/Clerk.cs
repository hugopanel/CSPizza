using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace WpfApp1.Models
{
    internal class Clerk : Worker
    {
        /// <summary>
        /// The Customer the Clerk is handling.
        /// </summary>
        private Customer? _currentCustomer;

        /// <summary>
        /// Gets a Customer object from a telephone number.
        /// 
        /// Returns null if the Customer does not exist yet.
        /// </summary>
        /// <param name="telephoneNumber">Phone number of the customer.</param>
        /// <returns>The customer or null.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Customer? GetCustomer(string telephoneNumber)
        {
            throw new NotImplementedException();
        }

        public Customer AddNewCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Clerk's main loop. 
        /// The Clerk will check for messages and handle them accordingly.
        /// </summary>
        public void Word()
        {
            // Check for an incoming phone call. 

            // Check for a 
            // MessageType.KitchenOrderReady
            // message from the cooks. 

            // Check for a 
        }

        /// <summary>
        /// Handles a phone call with a Customer. The Clerk will handle the customer from the moment it calls to the moment is hangs up.
        /// </summary>
        /// <param name="callId"></param>
        public void HandleCustomer(int callId)
        {
            //TODO: Finish this function

            // Ask the Customer if this is their first order

            // Ask the Customer for their phone number

            // If this is the first order, ask for their information
            // Store the information

            // If this is not their first order, send them their information and ask if it is still correct
            // If it isn't, ask to send the info again and store it

            // Tell the Customer to send you its order

            {
                // If the Customer says:
                // MessageType.AskDisplayFullOrder
                // Then send them their order

                // If the Customer says:
                // MessageType.AddToOrder
                // Then check if the menu item exists.
                // If so, add their item to the Order
                // If not, send them an error.

                // If the Customer says:
                // MessageType.RemoveFromOrder
                // Then check if the item is in the order. If so, remove it. Otherwise, do nothing.

                // If the Customer says:
                // MessageType.ConfirmOrder
                // Then confirm the order and send them a confirmation that the order is being prepared.
            }

            // Send a message to the Cooks to tell them to prepare the command ASAP.

            // The rest should be handled in another function.
        }
    }
}
