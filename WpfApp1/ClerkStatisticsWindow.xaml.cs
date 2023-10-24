﻿using System;
using System.Collections;
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