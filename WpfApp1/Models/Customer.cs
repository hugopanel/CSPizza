using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Modules;

namespace WpfApp1.Models
{
    public class Customer
    {
        public CustomerInfo CustomerInfo { get; set; }
        public string? Address { get; set; }

        public float CumulativeAmount { get; set; }

        public float AverageOrder {  get; set; }

        public Customer(CustomerInfo customerInfo)
        {
            CustomerInfo = customerInfo;
            CumulativeAmount = 0;
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

        [JsonConstructor]
        public Customer(string surname, string firstName, string telephoneNumber, DateOnly firstOrderDate, string Address, float CumulativeAmount, float AverageOrder)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber, firstOrderDate);
            this.Address = Address;
            this.CumulativeAmount = CumulativeAmount;
            this.AverageOrder = AverageOrder;
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

        public void UpdateAverageOrder(List<Order> orders)
        {
            float totalAmount = 0;
            int orderCount = 0;

            foreach (Order order in orders)
            {
                if (order.Customer == this)
                {
                    totalAmount += order.getPrice();
                    orderCount++;
                }
            }

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
