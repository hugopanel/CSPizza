using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour ClerkStatisticsWindow.xaml
    /// </summary>
    public partial class ClerkStatisticsWindow : Window
    {
        private Statistics stats;
        private List<Customer> CustomerList;
        private List<Order> OrderList;
        private List<Worker> ClerkList;
        public ClerkStatisticsWindow()
        {
            InitializeComponent();
            stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            CustomerList = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910", currentDate), "Paris, 15 rue de Genshin"),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405", currentDate), "Paris, 3 rue Minoring"),
                new Customer(new CustomerInfo("David", "Pauline", "0605040302", currentDate), "Bourg-la-Reine, 10 rue du 8 mai 1945")
            };
            Clerk Arthur = new Clerk();
            Arthur.Name = "Arthur";
            Arthur.Address = "Villejuif, 18 rue du Général Leclerc";
            Arthur.AddNewCustomer(CustomerList[0]);
            Clerk Asma = new Clerk();
            Asma.Name = "Asma";
            Asma.Address = "Vitry, 30 avenue de Villejuif";
            Asma.AddNewCustomer(CustomerList[1]);
            ClerkList = new List<Worker>
            {
                Arthur,Asma
            };
            OrderList = new List<Order>
            {
                new Order(Arthur)
                {
                    Customer = CustomerList[0]
                },new Order(Asma)
                {
                    Customer = CustomerList[1]
                },
                new Order(Asma)
                {
                    Customer = CustomerList[2]
                },
            };
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void CloseMinBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // To make sure you can still drag and drop frame after full screen
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void MainClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MainMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void MainMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void UpdateDataGrid(IEnumerable<Worker> clerks)
        {
            DataGridClerks.ItemsSource = clerks;
        }

        private void BtnOrderAlphabetical_Click(object sender, RoutedEventArgs e)
        {
            // Handle sorting by alphabetical order
            ClerkList = stats.OrderWorkerBy(ClerkList, "alphabetical");
            UpdateDataGrid(ClerkList);
            // Implement the sorting logic here and update the DataGrid
        }

        private void BtnOrderCity_Click(object sender, RoutedEventArgs e)
        {
            // Handle sorting by city
            ClerkList = stats.OrderWorkerBy(ClerkList, "city");
            UpdateDataGrid(ClerkList);
            // Implement the sorting logic here and update the DataGrid
        }

        private void BtnOrderNbOrders_Click(object sender, RoutedEventArgs e)
        {
            ClerkList = stats.OrderWorkerByNumberOrders(ClerkList);
            UpdateDataGrid(ClerkList);
        }
    }
}