using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using WpfApp1.Modules;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private Customer? _currentCustomer = null;

        public LoginView(string telephoneNumber, bool isNewUser)
        {
            InitializeComponent();

            // Check if we can find the user
            if (Pizzeria.Customers.Exists(c => c.CustomerInfo.TelephoneNumber == telephoneNumber))
            {
                // User exists
                lblWindowTitle.Text = "LOGIN";
                lblFormTitle.Text = "LOGIN";
                lblFormDescription.Text = "PLEASE UPDATE EVERY INCORRECT INFORMATION";

                // Retrieve the customer
                var customer = Pizzeria.Customers.Find(c => c.CustomerInfo.TelephoneNumber == telephoneNumber)!;
                
                txtSurname.Text = customer.CustomerInfo.Surname;
                txtFirstName.Text = customer.CustomerInfo.FirstName;
                txtAddress.Text = customer.Address;
                txtPurchDate.Text = customer.CustomerInfo.FirstOrderDate.ToString();
            }
            else
            {
                // We check if the user said they already ordered here...
                if (isNewUser)
                {
                    // The user never ordered.
                    txtPurchDate.Text = DateOnly.FromDateTime(DateTime.Now).ToString();
                }
            }

            txtNumber.Text = telephoneNumber;
        }

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

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            // Check if every field is correct
            if (txtSurname.Text.Length == 0 || txtFirstName.Text.Length == 0 || txtAddress.Text.Length == 0 ||
                txtNumber.Text.Length == 0 || txtPurchDate.Text.Length == 0)
            {
                MessageBox.Show(this, "Please fill in every field.", "Can't proceed.", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (!txtNumber.Text.All(char.IsDigit))
            {
                MessageBox.Show(this, "The phone number must be a valid number (without '+' or '(' or ')').", "Can't proceed.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime dDate;

            if (!DateTime.TryParse(txtPurchDate.Text, out dDate))
            {
                MessageBox.Show(this, "The purchase date is not in the right format!", "Can't proceed.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Subscribe to the next event
            App.RibbitMq.Subscribe(MessageType.AskIsInfoCorrect, HandleAskIsInfoCorrect);

            // Send the message
            var customer = new Customer(txtSurname.Text, txtFirstName.Text, txtNumber.Text, DateOnly.FromDateTime(dDate), txtAddress.Text);
            _currentCustomer = customer;
            App.RibbitMq.Send(new Message {MessageType = MessageType.AnswerInfo, Content = customer});
        }

        private async Task HandleAskIsInfoCorrect(IMessage<MessageType> message)
        {
            // Ask the Customer if the info is correct
            var messageBoxResult = MessageBox.Show("Are you sure the info is correct?", "Message from Clerk", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                // We are good to continue
                App.RibbitMq.Unsubscribe(MessageType.AskIsInfoCorrect, HandleAskIsInfoCorrect);
                App.RibbitMq.Send(new Message {MessageType = MessageType.AnswerIsInfoCorrect, Content = true});

                // We can open the main window and close this one...
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MainView mainView = new MainView(_currentCustomer);
                    mainView.Show();
                    this.Close();
                });
            }
            else
            {
                // The user wants to change the info
                App.RibbitMq.Send(new Message { MessageType = MessageType.AnswerIsInfoCorrect, Content = false });
            }
        }
    } 
}
