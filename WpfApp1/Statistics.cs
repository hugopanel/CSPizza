using System;
using System.Linq;
using System.Collections.Generic;
using WpfApp1.Models;

namespace WpfApp1
{
    public class Statistics
    {
        // public List<Customer> OrderCustomerBy(List<Customer> CustomerList, params string[] list)
        // {
        //     string[] parameters = { "alphabetical", "city", "cumulative" };
        //     List<Customer> orderedCustomers = CustomerList;

        //     foreach (string param in list)
        //     {
        //         if (param == "alphabetical")
        //         {
        //             orderedCustomers = orderedCustomers.OrderBy(customer => customer.getName()).ToList();
        //         }
        //         else if (param == "city")
        //         {
        //             orderedCustomers = orderedCustomers.OrderBy(customer => customer.getCity()).ToList();
        //         }
        //         else if (param == "cumulative")
        //         {
        //             orderedCustomers = orderedCustomers.OrderBy(customer => customer.getCumulativeAmount()).ToList();
        //         }
        //     }

        //     return orderedCustomers;
        // }

        // public float AverageOrderPrice(List<Order> Orders)
        // {
        //     float total = 0;
        //     foreach (Order order in Orders)
        //     {
        //         total += order.getPrice();
        //     }
        //     return total / Orders.Count;
        // }

        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            List<Customer> CustomerList = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910"), new Address("Genshin Street","Kuala Lumpur", 1200000)),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405"), new Address("Minoring Avenue","Paris", 1500000))
            };
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer);
            }
        }
    }
}
