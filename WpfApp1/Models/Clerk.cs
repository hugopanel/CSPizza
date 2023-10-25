using RibbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using WpfApp1.View;

namespace WpfApp1.Models
{
    public class Clerk : Worker
    {
        /// <summary>
        /// The Customer the Clerk is handling.
        /// </summary>
        private Customer? _currentCustomer;

        /// <summary>
        /// Only used when receiving a call.
        /// Null: We don't know.
        /// True: The customer is new.
        /// False: The customer isn't, we need to retrieve their info.
        /// </summary>
        private bool? _isCurrentCustomerNew = null;

        private string? _currentCustomerNumber = null;

        [JsonConstructor]
        public Clerk(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Clerk()
        {

        }

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

        public void AddNewCustomer(Customer customer)
        {
            _currentCustomer = customer;
        }

        /// <summary>
        /// The Clerk's main loop. 
        /// The Clerk will check for messages and handle them accordingly.
        /// </summary>
        public void Work()
        {
            // Check for a
            // MessageType.InitialCall
            // message from a new Customer.

            // Check for a 
            // MessageType.KitchenOrderReady
            // message from the cooks. 

            // Check for a 
            // MessageType.DeliveryArrivedAtPizzeria
            // message from the DeliveryMan.
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
        public int GetNumberOfOrders(List<Order> OrderList)
        {
            int nb = 0;
            foreach (Order order in OrderList)
            {
                nb = order.IsToClerk(this, nb);
            }
            return nb;

        }

        private async Task ClerkHandleInitialCallAsync(IMessage<MessageType> message)
        {
            // We get the dispatcher to access the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                // We get the WelcomeView window
                WelcomeView welcomeView = (WelcomeView)Application.Current.MainWindow;

                // We add text to the richtextbox
                welcomeView.AddText("Clerk" + Id + ": " + "Hello and welcome to Pizzeria Hulopa! Are you a new customer? (yes/no)");
            }, null);

            App.RibbitMq.Unsubscribe(MessageType.InitialCall, ClerkHandleInitialCallAsync);
            App.RibbitMq.Subscribe(MessageType.AnswerFirstOrder, ClerkHandleAnswerFirstOrder);
            // We ask the customer if it's their first order
            App.RibbitMq.Send(new Message { MessageType = MessageType.AskFirstOrder });
        }

        private async Task ClerkHandleAnswerFirstOrder(IMessage<MessageType> message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                WelcomeView welcomeView = (WelcomeView)Application.Current.MainWindow;

                if ((string) message.Content! == "yes")
                {
                    welcomeView.AddText("Clerk" + Id + ": " + "Welcome! Please, tell me your phone number so I can create you an account.");
                    _isCurrentCustomerNew = true;
                }
                else if ((string) message.Content! == "no")
                {
                    welcomeView.AddText("Clerk" + Id + ": " + "Welcome back! Please, tell me the phone number you usually use.");
                    _isCurrentCustomerNew = false;
                }
            }, null);

            App.RibbitMq.Unsubscribe(MessageType.AnswerFirstOrder, ClerkHandleAnswerFirstOrder);
            App.RibbitMq.Subscribe(MessageType.AnswerPhoneNumber, ClerkHandleAnswerPhoneNumber);
            App.RibbitMq.Send(new Message { MessageType = MessageType.AskPhoneNumber });
        }

        private async Task ClerkHandleAnswerPhoneNumber(IMessage<MessageType> message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                WelcomeView welcomeView = (WelcomeView)Application.Current.MainWindow;

                if (_isCurrentCustomerNew == true)
                {
                    welcomeView.AddText("Clerk" + Id + ": " + "Thank you! Please fill in the form so I know how to process your request.");
                }
                else
                {
                    welcomeView.AddText("Clerk" + Id + ": " + "Thank you! Please review the information I have and correct it if needed.");
                }
            }, null);

            _currentCustomerNumber = (string) message.Content!;
            App.RibbitMq.Unsubscribe(MessageType.AnswerPhoneNumber, ClerkHandleAnswerPhoneNumber);
            App.RibbitMq.Subscribe(MessageType.AnswerInfo, ClerkHandleAnswerInfo);
            App.RibbitMq.Send(new Message { MessageType = MessageType.AskInfo, Content = new object[] {(bool)_isCurrentCustomerNew!, (string)message.Content!} }); // TODO: Documenter ça
        }

        private async Task ClerkHandleAnswerInfo(IMessage<MessageType> message)
        {
            _currentCustomer = (Customer) message.Content!;

            // We need to ask the customer if their information is correct
            App.RibbitMq.Unsubscribe(MessageType.AnswerInfo, ClerkHandleAnswerInfo);
            App.RibbitMq.Subscribe(MessageType.AnswerIsInfoCorrect, ClerkHandleAnswerIsInfoCorrect);
            App.RibbitMq.Send(new Message() {MessageType = MessageType.AskIsInfoCorrect});
        }

        private async Task ClerkHandleAnswerIsInfoCorrect(IMessage<MessageType> message)
        {
            if ((bool) message.Content! == true)
            {
                // We can save the info of the customer
                try
                {
                    // Try to remove the customer if they already exist so we can replace them
                    Customer? customer = Pizzeria.Customers.Find(c =>
                        c.CustomerInfo.TelephoneNumber == _currentCustomerNumber);

                    if (customer != null)
                        Pizzeria.Customers.Remove(customer);
                }
                catch
                {
                    Console.WriteLine("Could not remove customer...");
                }
                
                Pizzeria.Customers.Add(_currentCustomer);

                App.RibbitMq.Unsubscribe(MessageType.AnswerIsInfoCorrect, ClerkHandleAnswerIsInfoCorrect);

                // TODO: Subscribe to the other events.
            }
            else
            {
                // The user wants to change the info again.
                App.RibbitMq.Subscribe(MessageType.AnswerInfo, ClerkHandleAnswerInfo);
                App.RibbitMq.Unsubscribe(MessageType.AnswerIsInfoCorrect, ClerkHandleAnswerIsInfoCorrect);
            }
        }

        public void Register()
        {
            App.RibbitMq.Subscribe(MessageType.InitialCall, ClerkHandleInitialCallAsync);
        }
    }
}
