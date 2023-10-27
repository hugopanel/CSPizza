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

        private Order? _currentOrder;

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
                    Console.WriteLine("Could not remove customer! Moving on...");
                }
                
                Pizzeria.Customers.Add(_currentCustomer!);

                App.RibbitMq.Unsubscribe(MessageType.AnswerIsInfoCorrect, ClerkHandleAnswerIsInfoCorrect);

                // We subscribe to the SubmitOrder event
                App.RibbitMq.Subscribe(MessageType.SubmitOrder, ClerkHandleSubmitOrder);
                // TODO: Subscribe to the HangUp event, when the user closes the app
            }
            else
            {
                // The user wants to change the info again.
                App.RibbitMq.Subscribe(MessageType.AnswerInfo, ClerkHandleAnswerInfo);
                App.RibbitMq.Unsubscribe(MessageType.AnswerIsInfoCorrect, ClerkHandleAnswerIsInfoCorrect);
            }
        }

        /// <summary>
        /// The Clerk receives the order from the customer.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ClerkHandleSubmitOrder(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("Received an order...", this);

            // We retrieve the order
            Order order = (Order) message.Content!;

            order.Status = OrderStatus.Taken; // Change the state of the order
            order.Clerk = this; // Assign this clerk to the order

            _currentOrder = order; // We'll want to use the _currentOrder variable later to refer to the order

            Pizzeria.Orders.Add(_currentOrder); // Add the order to the list of orders

            App.RibbitMq.Unsubscribe(MessageType.SubmitOrder, ClerkHandleSubmitOrder);

            // We tell the Customer
            App.RibbitMq.Send(new Message() { MessageType = MessageType.ConfirmationSubmitOrder, Content = _currentOrder.Id });

            // We can now send a message to the cooks to tell them a new order is waiting to be cooked.
            SendKitchenNewOrder(); // We don't want to await that call!
        }

        private bool _isWaitingForCooksAnswer;

        /// <summary>
        /// Send a message to the cooks once every 5 seconds for as long a no one sends an answer back.
        /// </summary>
        /// <returns></returns>
        private async Task SendKitchenNewOrder()
        {
            _isWaitingForCooksAnswer = true;
            App.RibbitMq.Subscribe(MessageType.KitchenPreparingOrder, HandleKitchenPreparingOrder);

            while (_isWaitingForCooksAnswer)
            {
                HomeView.ClerkPrintText("Sending a new message to the kitchen...", this);
                App.RibbitMq.Send(new Message() {MessageType = MessageType.KitchenNewOrder, Content = _currentOrder, SendType = SendType.FirstFree});
                await Task.Delay(5000); // Wait for 5 seconds before sending the message again.
            }
        }

        private async Task HandleKitchenPreparingOrder(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The kitchen is preparing the order...", this);

            // We must check if the message is for us 
            if ((int) message.Content! == _currentOrder!.Id)
            {
                // We know one cook took the order
                _isWaitingForCooksAnswer = false;

                App.RibbitMq.Unsubscribe(MessageType.KitchenPreparingOrder, HandleKitchenPreparingOrder);
                App.RibbitMq.Subscribe(MessageType.KitchenOrderReady, HandleKitchenOrderReady);

                // We can inform the Customer
                App.RibbitMq.Send(new Message() {MessageType = MessageType.ClerkOrderPreparing, Content = _currentOrder.Id});

                // We update the Order
                _currentOrder.Status = OrderStatus.Preparing;
            }
        }

        private async Task HandleKitchenOrderReady(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The order is ready!", this);

            // We must check if the message is for us
            if ((int) message.Content! == _currentOrder!.Id)
            {
                // It is

                App.RibbitMq.Unsubscribe(MessageType.KitchenOrderReady, HandleKitchenOrderReady);

                // We can inform the customer
                App.RibbitMq.Send(new Message()
                    {MessageType = MessageType.ClerkOrderWaiting, Content = _currentOrder.Id});

                // We update the order
                _currentOrder.Status = OrderStatus.Waiting;

                // Now we tell the DeliveryMen
                SendDeliveryNewOrder();
            }
        }

        private bool _isWaitingForDeliveryMansAnswer;
        
        /// <summary>
        /// Send a message to the delivery men once every 5 seconds for as long a no one sends an answer back.
        /// </summary>
        /// <returns></returns>
        private async Task SendDeliveryNewOrder()
        {
            _isWaitingForDeliveryMansAnswer = true;

            App.RibbitMq.Subscribe(MessageType.DeliveryGoing, HandleDeliveryGoing);
            App.RibbitMq.Subscribe(MessageType.DeliveryArrivedToCustomer, HandleDeliveryArrivedToCustomer);
            App.RibbitMq.Subscribe(MessageType.DeliveryDropped, HandleDeliveryDropped);

            while (_isWaitingForDeliveryMansAnswer)
            {
                HomeView.ClerkPrintText("Searching for a free delivery man...", this);

                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.DeliveryNewOrder, 
                    Content = _currentOrder, 
                    SendType = SendType.FirstFree
                });

                await Task.Delay(5000); // We wait 5 seconds before sending the message again.
            }
        }

        private async Task HandleDeliveryGoing(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The delivery man left the pizzeria!", this);

            // We check if the message was for us
            if ((int) message.Content! == _currentOrder!.Id)
            {
                _isWaitingForDeliveryMansAnswer = false;

                App.RibbitMq.Unsubscribe(MessageType.DeliveryGoing, HandleDeliveryGoing);

                // We can inform the Customer
                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.ClerkOrderDelivering,
                    Content = _currentOrder.Id
                });

                // We update the order
                _currentOrder.Status = OrderStatus.Delivering;
            }
        }

        private async Task HandleDeliveryArrivedToCustomer(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The delivery man arrived at the customer's house!", this);

            // We check if the message is for us
            if ((int) message.Content! == _currentOrder!.Id)
            {
                // The order has been delivered!

                App.RibbitMq.Subscribe(MessageType.DeliveryArrivedAtPizzeria, HandleDeliveryArrivedAtPizzeria);
                App.RibbitMq.Unsubscribe(MessageType.DeliveryArrivedToCustomer, HandleDeliveryArrivedToCustomer);
                App.RibbitMq.Unsubscribe(MessageType.DeliveryDropped, HandleDeliveryDropped);
                
                // We can inform the Customer
                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.ClerkOrderDelivered,
                    Content = _currentOrder.Id
                });

                // And update the order
                _currentOrder.Status = OrderStatus.Delivered;
            }
        }

        private async Task HandleDeliveryDropped(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The delivery man dropped the order! :(", this);

            // We check if the message is for us
            if ((int) message.Content! == _currentOrder!.Id)
            {
                // Sadly for the customer, it is... :(

                App.RibbitMq.Unsubscribe(MessageType.DeliveryDropped, HandleDeliveryDropped);
                App.RibbitMq.Unsubscribe(MessageType.DeliveryArrivedAtPizzeria, HandleDeliveryArrivedAtPizzeria);

                // We should probably tell the Customer (or not?)
                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.DeliveryDropped,
                    Content = _currentOrder.Id
                });

                // And update their order
                _currentOrder.Status = OrderStatus.Dropped;

                // We hang up
                HangUp();
            }
        }

        private async Task HandleDeliveryArrivedAtPizzeria(IMessage<MessageType> message)
        {
            HomeView.ClerkPrintText("The delivery man came back at the pizzeria...", this);

            // We check if the message is for us
            if ((int) message.Content! == _currentOrder!.Id)
            {
                // It is! And the order is complete!

                App.RibbitMq.Unsubscribe(MessageType.DeliveryArrivedAtPizzeria, HandleDeliveryArrivedAtPizzeria);

                // The Clerk takes the money
                HomeView.ClerkPrintText("I got the money.", this);

                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.ClerkOrderDone,
                    Content = _currentOrder.Id,
                    From = this
                });

                // The Clerk tells the Customer
                App.RibbitMq.Send(new Message()
                {
                    MessageType = MessageType.ClerkOrderDone,
                    Content = _currentOrder.Id
                });

                // We update the Order
                _currentOrder.Status = OrderStatus.Done;

                // We hang up
                HangUp();
            }
        }

        /// <summary>
        /// Reset the _current variables (order, customer, etc.) so the Clerk is ready to answer another call.
        /// </summary>
        private void HangUp()
        {
            // We set the variables to null...
            _currentOrder = null;
            _currentCustomer = null;
            _currentCustomerNumber = null;
            _isCurrentCustomerNew = null;

            // ... and we subscribe the Clerk to the initial call event
            App.RibbitMq.Subscribe(MessageType.InitialCall, ClerkHandleInitialCallAsync);

            HomeView.ClerkPrintText("Ready to receive more orders!", this);
        }

        public override void CheckIn()
        {
            App.RibbitMq.Subscribe(MessageType.InitialCall, ClerkHandleInitialCallAsync);
        }

        public override void CheckOut()
        {
            // We don't need to do anything...
            // We could try to unsubscribe from every Clerk event.
        }
    }
}
