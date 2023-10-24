using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using WpfApp1.Models;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

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
                    orderedCustomers = orderedCustomers.OrderBy(customer => customer.Address).ToList();
                }
            }

            return orderedCustomers;
        }

        public List<Worker> OrderWorkerBy(List<Worker> WorkerList, params string[] list)
        {
            string[] parameters = { "alphabetical", "city" };
            List<Worker> orderedWorkers = WorkerList;

            foreach (string param in list)
            {
                if (param == "alphabetical")
                {
                    orderedWorkers = orderedWorkers.OrderBy(worker => worker.Name).ToList();
                }
                else if (param == "city")
                {
                    orderedWorkers = orderedWorkers.OrderBy(worker => worker.Address).ToList();
                }
            }

            return orderedWorkers;
        }

        public List<Customer> OrderCustomerByCumulativeAmount(List<Customer> CustomerList, List<Order> OrderList)
        {
            List<Customer> orderedCustomers = new List<Customer>(CustomerList);
            return orderedCustomers.OrderBy(customer => customer.CumulativeAmount).ToList();
        }

        public List<Worker> OrderWorkerByNumberOrders(List<Worker> WorkerList)
        {
            List<Worker> orderedWorkers = new List<Worker>(WorkerList);
            return orderedWorkers.OrderBy(worker => worker.NbOrders).ToList();
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

        public float setAverageOrder(Customer customer)
        {
            string fileNameOrders = "../../../Json/Orders.json";
            string jsonStringOrders = File.ReadAllText(fileNameOrders);
            List<Order> Orders = JsonConvert.DeserializeObject<List<Order>>(jsonStringOrders)!;
            float nbOrders = 0;
            float amount = 0;
            foreach (Order order in Orders)
            {
                if (order.Customer == customer)
                {
                    nbOrders++;
                    amount += order.getPrice();
                }
            }
            return amount/nbOrders;
        }

        public List<Order> OrderByTimePeriod(List<Order> Orders, DateTime d1, DateTime d2)
        {
            List<Order> ordersInTimePeriod = new List<Order>();
            foreach(Order order in Orders)
            {
                if(order.dateTime >=  d1 && order.dateTime <= d2)
                {
                    ordersInTimePeriod.Add(order);
                }
            }
            return ordersInTimePeriod;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            var stats = new Statistics();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            /*
            List<Customer> customers = new List<Customer>
            {
                new Customer(new CustomerInfo("Brunesseaux", "Lou", "0607080910", currentDate), "Paris, 15 rue de Genshin"),
                new Customer(new CustomerInfo("Panel", "Hugo", "0102030405", currentDate), "Paris, 3 rue Minoring"),
                new Customer(new CustomerInfo("David", "Pauline", "0605040302", currentDate), "Bourg-la-Reine, 10 rue du 8 mai 1945")
            };
            List<Customer> CustomerList = customers;

            Clerk Arthur = new Clerk();
            Arthur.Name = "Arthur";
            Arthur.Address = "Villejuif, 18 rue du Général Leclerc";
            Arthur.AddNewCustomer(CustomerList[0]);
            Clerk Asma = new Clerk();
            Asma.Name = "Asma";
            Asma.Address = "Vitry, 30 avenue de Villejuif";
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
            OrderList[0].AddPizza(new Pizza("Pizza", 10000, new PizzaType("margarita",8), new PizzaSize("small",0)));
            OrderList[0].AddPizza(new Pizza("Pizza", 12000, new PizzaType("calzone", 10), new PizzaSize("medium", 1.5F)));
            OrderList[1].AddPizza(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("large", 3)));
            OrderList[2].AddPizza(new Pizza("Pizza", 10000, new PizzaType("margarita", 8), new PizzaSize("medium", 1.5F)));
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string strJson = JsonSerializer.Serialize<IList<Customer>>(CustomerList, opt);
            Console.WriteLine(strJson);
            opt = new JsonSerializerOptions() { WriteIndented = true };
            strJson = JsonSerializer.Serialize<IList<Clerk>>(ClerkList, opt);
            Console.WriteLine(strJson);
            opt = new JsonSerializerOptions() { WriteIndented = true };
            strJson = JsonSerializer.Serialize<IList<Order>>(OrderList, opt);
            Console.WriteLine(strJson);
            */
            DateTime currentDateh = DateTime.Now;
            Console.WriteLine(currentDateh.ToString("yyyy-MM-ddTHH:mm:ss"));

            string fileName = "../../../Json/Customers.json";
            string jsonString = File.ReadAllText(fileName);
            List<Customer> CustomerList = JsonConvert.DeserializeObject<List<Customer>>(jsonString)!;
            string fileNameClerks = "../../../Json/Clerks.json";
            string jsonStringClerks = File.ReadAllText(fileNameClerks);
            List<Clerk> ClerkList = JsonConvert.DeserializeObject<List<Clerk>>(jsonStringClerks)!;
            string fileNameOrders = "../../../Json/Orders.json";
            string jsonStringOrders = File.ReadAllText(fileNameOrders);
            List<Order> OrderList = JsonConvert.DeserializeObject<List<Order>>(jsonStringOrders)!;

            foreach(Customer customer in CustomerList)
            {
                customer.UpdateAverageOrder(OrderList);
            }

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
                Console.WriteLine(customer.CustomerInfo.ToString() + ", " + customer.Address.ToString() + ", Cumulative amount :"+customer.CumulativeAmount);
            }
            Console.WriteLine("\nNumber of orders by clerk :");
            foreach (Clerk clerk in ClerkList)
            {
                Console.WriteLine("Number of orders for "+clerk.Name + " : "+clerk.NbOrders);
            }
            Console.WriteLine("\nAverage price of orders :" + stats.AverageOrderPrice(OrderList));
            DateTime d1 = new DateTime(2023, 10, 23, 10, 40, 18);
            DateTime d2 = new DateTime(2023, 10, 23, 11, 59, 18);
            List<Order> orders = stats.OrderByTimePeriod(OrderList, d1, d2);
            Console.WriteLine("Orders by time period :");
            foreach(Order order in orders) { 
                Console.WriteLine("Order : "+order.Id + " at "+order.dateTime);
            }
        }
    }
}
