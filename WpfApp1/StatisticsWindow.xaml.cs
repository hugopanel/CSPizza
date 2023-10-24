using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using WpfApp1.Models;
using System;
using WpfApp1.Modules;

namespace WpfApp1
{
    public partial class StatisticsWindow : Window
    {
        private Statistics stats;
        private DateTime? selectedDate1;
        private DateTime? selectedDate2;


        public StatisticsWindow()
        {
            InitializeComponent();
            stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            /*
            CustomerList = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910", currentDate), "Kuala Lumpur, Genshin Street"),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405", currentDate), "Paris, Minoring Avenue"),
                new Customer(new CustomerInfo("David", "Pauline", "0605040302", currentDate), "Sain-Michel-Chef-Chef, Karmine Boulevard")
            };
            Clerk Arthur = new Clerk();
            Arthur.Name = "Arthur";
            Arthur.AddNewCustomer(CustomerList[0]);
            Clerk Asma = new Clerk();
            Asma.Name = "Asma";
            Asma.AddNewCustomer(CustomerList[1]);
            List<Clerk> ClerkList = new List<Clerk>
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
            OrderList[0].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("small", 0)));
            OrderList[0].AddItem(new Pizza("Pizza", 12000, new PizzaType("calzone", 10), new PizzaSize("medium", 1.5F)));
            OrderList[1].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("large", 3)));
            OrderList[2].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("medium", 1.5F)));*/
            FileModule.LoadCustomers();
            FileModule.LoadOrders();
            foreach(Customer customer in Pizzeria.Customers)
            {
                customer.UpdateAverageOrder();
            }
        }

        private void UpdateDataGrid(IEnumerable<Customer> customers)
        {
            DataGridCustomers.ItemsSource = customers;
        }

        private void UpdateDataGridOrder(IEnumerable<Order> orders)
        {
            DataGridOrders.ItemsSource = orders;
        }


        private void BtnOrderAlphabetical_Click(object sender, RoutedEventArgs e)
        {
            Pizzeria.Customers = stats.OrderCustomerBy(Pizzeria.Customers, "alphabetical");
            UpdateDataGrid(Pizzeria.Customers);
        }

        private void BtnOrderCity_Click(object sender, RoutedEventArgs e)
        {
            Pizzeria.Customers = stats.OrderCustomerBy(Pizzeria.Customers, "city");
            UpdateDataGrid(Pizzeria.Customers);
        }

        private void BtnOrderCumulativeAmount_Click(object sender, RoutedEventArgs e)
        {
            Pizzeria.Customers = stats.OrderCustomerByCumulativeAmount(Pizzeria.Customers, Pizzeria.Orders);
            UpdateDataGrid(Pizzeria.Customers);
        }

        private void BtnAverageOrderPrice_Click(object sender, RoutedEventArgs e)
        {
            float averagePrice = stats.AverageOrderPrice(Pizzeria.Orders);
            ListBoxResults.Items.Clear();
            ListBoxResults.Items.Add($"Average Order Price: {averagePrice:C}");
        }

        private void BtnShowClerkStatistics_Click(object sender, RoutedEventArgs e)
        {
            ClerkStatisticsWindow clerkStatisticsWindow = new ClerkStatisticsWindow();
            clerkStatisticsWindow.Show();
        }

        private void BtnShowDelivererStatistics_Click(object sender, RoutedEventArgs e)
        {
            DeliveryStatisticsWindow deliveryStatisticsWindow = new DeliveryStatisticsWindow();
            deliveryStatisticsWindow.Show();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;

            if (datePicker.Name == "d1")
            {
                selectedDate1 = d1.SelectedDate;
            }
            else if (datePicker.Name == "d2")
            {
                selectedDate2 = d2.SelectedDate;
            }

            if (selectedDate1.HasValue && selectedDate2.HasValue)
            {
                DateTime date1 = selectedDate1.Value;
                DateTime date2 = selectedDate2.Value;
                if (date1 <= date2)
                {
                    List<Order> OrderByTimePeriod = stats.OrderByTimePeriod(Pizzeria.Orders, date1, date2);
                    UpdateDataGridOrder(OrderByTimePeriod);
                }
            }
        }
    }
}
