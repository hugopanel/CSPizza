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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour DeliveryStatisticsWindow.xaml
    /// </summary>
    public partial class DeliveryStatisticsWindow : Window
    {
        private Statistics stats;
        private List<Customer> CustomerList;
        private List<Order> OrderList;
        private List<Worker> ClerkList;
        private List<Worker> DeliverersList;
        public DeliveryStatisticsWindow()
        {
            InitializeComponent();
            stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            CustomerList = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910", currentDate), new Address("Genshin Street","Kuala Lumpur", 1200000)),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405", currentDate), new Address("Minoring Avenue","Paris", 1500000)),
                new Customer(new CustomerInfo("David", "Pauline", "0605040302", currentDate), new Address("Karmine Boulevard","Saint-Michel-Chef-Chef", 1500000))
            };
            Clerk Arthur = new Clerk();
            Arthur.Name = "Arthur";
            Arthur.Address = new Address("30-32 Av. de la République", "Villejuif", 10000);
            Arthur.AddNewCustomer(CustomerList[0]);
            Clerk Asma = new Clerk();
            Asma.Name = "Asma";
            Asma.Address = new Address("136 bis Bd Maxime Gorki", "Villejuif", 9000);
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
            DeliveryMan Jess = new DeliveryMan();
            Jess.Name = "Jess";
            Jess.Address = new Address("136 bis Bd Maxime Gorki", "Villejuif", 9000);
            DeliveryMan Fanny = new DeliveryMan();
            Fanny.Name = "Fanny";
            Fanny.Address = new Address("30-32 Av. de la République", "Villejuif", 10000);
            DeliverersList = new List<Worker>
            {
                Jess,Fanny
            };
            Jess.Deliver(OrderList[0]);
            Fanny.Deliver(OrderList[1]); 
            Jess.Deliver(OrderList[2]);
        }

        private void UpdateDataGrid(IEnumerable<Worker> deliverers)
        {
            DataGridDeliverer.ItemsSource = deliverers;
        }

        private void BtnOrderAlphabetical_Click(object sender, RoutedEventArgs e)
        {
            DeliverersList = stats.OrderWorkerBy(DeliverersList, "alphabetical");
            UpdateDataGrid(DeliverersList);
        }

        private void BtnOrderCity_Click(object sender, RoutedEventArgs e)
        {
            DeliverersList = stats.OrderWorkerBy(DeliverersList, "city");
            UpdateDataGrid(DeliverersList);
        }

        private void BtnOrderNbOrders_Click(object sender, RoutedEventArgs e)
        {
            DeliverersList = stats.OrderWorkerByNumberOrders(DeliverersList);
            UpdateDataGrid(DeliverersList);
        }
    }
}
