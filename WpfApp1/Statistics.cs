using System;
using System.Linq;
using System.Collections.Generic;
using WpfApp1.Models;

namespace WpfApp1
{
    public class Statistics
    {
        public List<Customer> OrderCustomerBy(List<Customer> CustomerList, params string[] list)
        {
            string[] parameters = { "alphabetical", "city" };
            List<Customer> orderedCustomers = CustomerList;
            
            foreach (string param in list)
            {
                if (param == "alphabetical")
                {
                    orderedCustomers = orderedCustomers.OrderBy(customer => customer.CustomerInfo.Surname).ToList();
                }
                else if (param == "city")
                {
                    orderedCustomers = orderedCustomers.OrderBy(customer => customer.Address.City).ToList();
                }
            }

            return orderedCustomers;
        }

        public List<Customer> OrderCustomerByCumulativeAmount(List<Customer> CustomerList, List<Order> OrderList)
        { 
            return CustomerList.OrderBy(customer => customer.GetCumulativeAmount(OrderList)).ToList(); ;
        }


        public float AverageOrderPrice(List<Order> Orders)
        {
             float total = 0;
             foreach (Order order in Orders)
             {
                 total += order.getPrice();
             }
             return total / Orders.Count;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            var stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            List<Customer> CustomerList = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910", currentDate), new Address("Genshin Street","Kuala Lumpur", 1200000)),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405", currentDate), new Address("Minoring Avenue","Paris", 1500000)),
                new Customer(new CustomerInfo("David", "Pauline", "0605040302", currentDate), new Address("Karmine Boulevard","Saint-Michel-Chef-Chef", 1500000))
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
            List<Order> OrderList = new List<Order>
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
            OrderList[0].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita",8), new PizzaSize("small",0)));
            OrderList[0].AddItem(new Pizza("Pizza", 12000, new PizzaType("calzone", 10), new PizzaSize("medium", 1.5F)));
            OrderList[1].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("large", 3)));
            OrderList[2].AddItem(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("medium", 1.5F)));
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer.CustomerInfo.ToString() + ", " + customer.Address.ToString());
            }
            CustomerList = stats.OrderCustomerBy(CustomerList, "alphabetical");
            Console.WriteLine("\nList by alphabetical order :");
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer.CustomerInfo.ToString() + ", " + customer.Address.ToString());
            }
            CustomerList = stats.OrderCustomerBy(CustomerList, "city");
            Console.WriteLine("\nList ordered by city :");
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer.CustomerInfo.ToString() + ", " + customer.Address.ToString());
            }
            CustomerList = stats.OrderCustomerByCumulativeAmount(CustomerList, OrderList);
            Console.WriteLine("\nList ordered by cumulative amount :");
            foreach (Customer customer in CustomerList)
            {
                Console.WriteLine(customer.CustomerInfo.ToString() + ", " + customer.Address.ToString() + ", Cumulative amount :"+customer.GetCumulativeAmount(OrderList));
            }
            Console.WriteLine("\nNumber of orders by clerk :");
            foreach (Clerk clerk in ClerkList)
            {
                Console.WriteLine("Number of orders for "+clerk.Name + " : "+clerk.GetNumberOfOrders(OrderList));
            }
            Console.WriteLine("\nAverage price of orders :" + stats.AverageOrderPrice(OrderList));
        }
    }
}
