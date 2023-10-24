using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;
using WpfApp1.Modules;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
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
                MessageBox.Show(this, "Please fill in every field.", "Can't register new user.", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            if (!txtNumber.Text.All(char.IsDigit))
            {
                MessageBox.Show(this, "The phone number must be a valid number (without '+' or '(' or ')').", "Can't register new user.", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DateTime dDate;

            if (!DateTime.TryParse(txtPurchDate.Text, out dDate))
            {
                MessageBox.Show(this, "The purchase date is not in the right format!", "Can't register new user.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(this, dDate.ToString(), "", MessageBoxButton.OK, MessageBoxImage.Information);
            Customer customer = new Customer(txtSurname.Text, txtFirstName.Text, txtNumber.Text, DateOnly.FromDateTime(dDate), txtAddress.Text);

            FileModule.Customers.Add(customer);
            FileModule.SaveCustomers();

            Application.Current.Shutdown();
        }
    } 
}
