using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RibbitMQ;
using WpfApp1.Models;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : Window
    {
        public WelcomeView()
        {
            InitializeComponent();

            rtb_Customer.TextChanged += richTextBox_TextChanged;

            // Start exchange
            AddText("Please type 'call' to call the pizzeria.");

            Clerk myClerk = Pizzeria.Clerks.First();
            //myClerk.Register();
            // TODO: Remplacer l'usage de la fonction Register !!

            txt_UserInput.Focus();
        }

        #region Window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void OnTextBoxKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                HandleCustomerMessages();

                AddText("> " + txt_UserInput.Text);
                txt_UserInput.Clear();
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            rtb_Customer.ScrollToEnd();
        }

        public new void AddText(string text)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(text));
            rtb_Customer.Document.Blocks.Add(paragraph);
        }
        #endregion

        #region Messages
        
        private MessageType? currentMessageType = null;

        /// <summary>
        /// Handles user input in the textbox to send messages and update the context. 
        /// </summary>
        private void HandleCustomerMessages()
        {
            if (currentMessageType == null && txt_UserInput.Text == "call")
            {
                App.RibbitMq.Send(new Message {MessageType = MessageType.InitialCall});

                App.RibbitMq.Subscribe(MessageType.AskFirstOrder, HandleAskFirstOrder);
                currentMessageType = MessageType.AskFirstOrder;
            } 
            else if (currentMessageType == MessageType.AskFirstOrder)
            {
                if (txt_UserInput.Text == "yes")
                {
                    // Never ordered
                    App.RibbitMq.Send(new Message {MessageType = MessageType.AnswerFirstOrder, Content = "yes"});
                } 
                else if (txt_UserInput.Text == "no")
                {
                    // Already ordered
                    App.RibbitMq.Send(new Message { MessageType = MessageType.AnswerFirstOrder, Content = "no" });
                }
            }
            else if (currentMessageType == MessageType.AskPhoneNumber)
            {
                // Check if the phone number is correct
                if (Regex.Match(txt_UserInput.Text, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$").Success)
                {
                    // Phone number is valid
                    App.RibbitMq.Send(new Message { MessageType = MessageType.AnswerPhoneNumber, Content = txt_UserInput.Text });
                }
                else
                {
                    // Phone number is not valid
                    AddText("Please enter a valid phone number.");
                }
            }
        }

        private async Task HandleAskFirstOrder(IMessage<MessageType> message)
        {
            currentMessageType = MessageType.AskFirstOrder; // Set the current message type to be able to handle user input in the correct context.
            
            App.RibbitMq.Unsubscribe(MessageType.AskFirstOrder, HandleAskFirstOrder); // Unsubscribe from the event
            App.RibbitMq.Subscribe(MessageType.AskPhoneNumber, HandleAskPhoneNumber); // Subscribe to the next event
        }

        private async Task HandleAskPhoneNumber(IMessage<MessageType> message)
        {
            currentMessageType = MessageType.AskPhoneNumber;

            App.RibbitMq.Unsubscribe(MessageType.AskPhoneNumber, HandleAskPhoneNumber);
            App.RibbitMq.Subscribe(MessageType.AskInfo, HandleAskInfo);

        }

        private async Task HandleAskInfo(IMessage<MessageType> message)
        {
            bool isCustomerNew = (bool) ((object[]) message.Content!)[0];
            string phoneNumber = (string) ((object[]) message.Content)[1];

            // Open the login window
            await Task.Delay(2000);
            Application.Current.Dispatcher.Invoke(() =>
            {
                App.Current.MainWindow = null;

                LoginView loginView = new LoginView(phoneNumber, isCustomerNew);
                loginView.Owner = this.Owner;
                loginView.Show();
                this.Close();
            }, null);

        }

        #endregion
    }
}
