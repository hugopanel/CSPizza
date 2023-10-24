using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Modules;
using System.ComponentModel;

namespace WpfApp1.Models
{
    public class Customer : INotifyPropertyChanged
    {
        public CustomerInfo CustomerInfo { get; set; }
        public string? Address { get; set; }

        public float CumulativeAmount { get; set; }

        public float AverageOrder { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Customer(CustomerInfo customerInfo)
        {
            CustomerInfo = customerInfo;
            CumulativeAmount = 0;
        }

        [JsonConstructor]
        public Customer(CustomerInfo customerInfo, string address, float cumulativeAmount)
        {
            CustomerInfo = customerInfo;
            Address = address;
            CumulativeAmount = cumulativeAmount;
        }

        public Customer(CustomerInfo customerInfo, string? address) : this(customerInfo)
        {
            Address = address;
            CumulativeAmount = 0;
        }

        public Customer(string surname, string firstName, string telephoneNumber)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber);
        }

        public Customer(string surname, string firstName, string telephoneNumber, DateOnly firstOrderDate)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber, firstOrderDate);
        }

        public Customer(string surname, string firstName, string telephoneNumber, DateOnly firstOrderDate,
            string address) : this(surname, firstName, telephoneNumber, firstOrderDate)
        {
            Address = address;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float GetCumulativeAmount(List<Order> OrderList)
        {
            float amount = 0;
            foreach (Order order in OrderList)
            {
                amount = order.addToCumulative(this, amount);
            }
            return amount;

        }


        public void UpdateAverageOrder()
        {
            float totalAmount = 0;
            int orderCount = 0;

            foreach (Order order in Pizzeria.Orders)
            {
                Console.WriteLine(order.Customer.CustomerInfo.Surname);
                if (order.Customer == this)
                {
                    totalAmount += order.getPrice();
                    orderCount++;
                }
            }
            Console.WriteLine(totalAmount.ToString());

            if (orderCount > 0)
            {
                AverageOrder = totalAmount / orderCount;
            }
            else
            {
                AverageOrder = 0;
            }
        }
    }
}
