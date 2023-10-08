using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Modules;

namespace WpfApp1.Models
{
    class Customer
    {
        public CustomerInfo CustomerInfo { get; set; }
        public Address? Address { get; set; }

        public Customer(CustomerInfo customerInfo)
        {
            CustomerInfo = customerInfo;
        }

        public Customer(CustomerInfo customerInfo, Address? address) : this(customerInfo)
        {
            Address = address;
        }

        public Customer(string surname, string firstName, string telephoneNumber)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber);
        }

        public Customer(string surname, string firstName, string telephoneNumber, DateOnly firstOrderDate)
        {
            CustomerInfo = new CustomerInfo(surname, firstName, telephoneNumber, firstOrderDate);
        }
    }
}
