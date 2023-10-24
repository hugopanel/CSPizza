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
        public Customer(string surname, string firstName, string telephoneNumber, DateOnly firstOrderDate, string Address, float CumulativeAmount)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber, firstOrderDate);
            this.Address = Address;
            this.CumulativeAmount = CumulativeAmount;
            Console.WriteLine(this.CumulativeAmount.ToString());
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
    }
}
