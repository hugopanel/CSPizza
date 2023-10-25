using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
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

            App.currentUiDispatcher = Application.Current.Dispatcher;

            rtb_Customer.TextChanged += richTextBox_TextChanged;

            // Start exchange
            AddText("Please type 'call' to call the pizzeria.");

            Clerk myClerk = Pizzeria.Clerks.First();
            myClerk.Register();
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
            }
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            rtb_Customer.ScrollToEnd();
            txt_UserInput.Clear();
        }

        private new void AddText(string text)
        {
            var paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run(text));
            rtb_Customer.Document.Blocks.Add(paragraph);
        }
        #endregion

        #region Messages
        
        private MessageType? currentMessageType = null;

        private void HandleCustomerMessages()
        {
            if (currentMessageType == null && txt_UserInput.Text == "call")
            {
                App.RibbitMq.Send(MessageType.InitialCall, new Message());
                Console.WriteLine("Sent message.");

                App.RibbitMq.Subscribe(MessageType.AskFirstOrder, HandleAskFirstOrder);
                currentMessageType = MessageType.AskFirstOrder;
            }
        }

        private async Task HandleAskFirstOrder(IMessage<MessageType> message)
        {
            Console.WriteLine("Received AskFirstOrder message!!!!");
        }

        public void AddTextNonUI(string text)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run(text));
                rtb_Customer.Document.Blocks.Add(paragraph);
            });
        }

        #endregion
    }
}
