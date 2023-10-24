using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows;
using WpfApp1.Models;

namespace WpfApp1.Modules
{
    internal class FileModule
    {
        public static List<Customer> Customers { get; set; } = new();
        public static List<Worker> Workforce { get; set; } = new();
        public static List<Item> Menu { get; set; } = new();

        public static void SaveCustomers()
        {
            string jsonCustomers = JsonSerializer.Serialize(Customers);
            File.WriteAllText("customers.json", jsonCustomers);
        }
    }
}
