﻿using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Models;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp1.View
{
    /// <summary>
    /// Interaction logic for StatView.xaml
    /// </summary>
    public partial class StatView : UserControl
    {
        private Statistics stats;
        private List<Customer> CustomerList;
        private List<Order> OrderList;
        private DateTime? selectedDate1;
        private DateTime? selectedDate2;
        public StatView()
        {
            InitializeComponent();
            stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            string fileName = "../../../Json/Customers.json";
            string jsonString = File.ReadAllText(fileName);
            CustomerList = JsonConvert.DeserializeObject<List<Customer>>(jsonString)!;
            /*string fileNameClerks = "../../../Json/Clerks.json";
            string jsonStringClerks = File.ReadAllText(fileNameClerks);
            ClerkList = JsonConvert.DeserializeObject<List<Clerk>>(jsonStringClerks)!;*/
            string fileNameOrders = "../../../Json/Orders.json";
            string jsonStringOrders = File.ReadAllText(fileNameOrders);
            OrderList = JsonConvert.DeserializeObject<List<Order>>(jsonStringOrders)!;
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
            CustomerList = stats.OrderCustomerBy(CustomerList, "alphabetical");
            UpdateDataGrid(CustomerList);
        }

        private void BtnOrderCity_Click(object sender, RoutedEventArgs e)
        {
            CustomerList = stats.OrderCustomerBy(CustomerList, "city");
            UpdateDataGrid(CustomerList);
        }

        private void BtnOrderCumulativeAmount_Click(object sender, RoutedEventArgs e)
        {
            CustomerList = stats.OrderCustomerByCumulativeAmount(CustomerList, OrderList);
            UpdateDataGrid(CustomerList);
        }

        private void BtnAverageOrderPrice_Click(object sender, RoutedEventArgs e)
        {
            float averagePrice = stats.AverageOrderPrice(OrderList);
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
                    List<Order> OrderByTimePeriod = stats.OrderByTimePeriod(OrderList, date1, date2);
                    UpdateDataGridOrder(OrderByTimePeriod);
                }
            }
        }


    }
}

