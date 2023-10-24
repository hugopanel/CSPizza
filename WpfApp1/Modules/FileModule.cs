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
        public static void LoadCustomers()
        {
            if (File.Exists("customers.json"))
                Pizzeria.Customers =
                    JsonSerializer.Deserialize<List<Customer>>(File.ReadAllText("customers.json"), App.jsonOptions);
            else
                Pizzeria.Customers = new();
        }

        public static void SaveCustomers()
        {
            var jsonCustomers = JsonSerializer.Serialize(Pizzeria.Customers, App.jsonOptions);
            File.WriteAllText("customers.json", jsonCustomers);
        }

        public static void LoadWorkforce()
        {
            Pizzeria.Clerks = new();
            Pizzeria.Cooks = new();
            Pizzeria.DeliveryMen = new();

            if (File.Exists("clerks.json"))
                Pizzeria.Clerks = JsonSerializer.Deserialize<List<Clerk>>(File.ReadAllText("clerks.json"), App.jsonOptions);
            if (File.Exists("cooks.json"))
                Pizzeria.Cooks = JsonSerializer.Deserialize<List<Cook>>(File.ReadAllText("cooks.json"), App.jsonOptions);
            if (File.Exists("deliverymen"))
                Pizzeria.DeliveryMen = JsonSerializer.Deserialize<List<DeliveryMan>>(File.ReadAllText("deliverymen.json"), App.jsonOptions);
        }

        public static void SaveWorkforce()
        {
            File.WriteAllText("clerks.json", JsonSerializer.Serialize(Pizzeria.Clerks, App.jsonOptions));
            File.WriteAllText("cooks.json", JsonSerializer.Serialize(Pizzeria.Cooks, App.jsonOptions));
            File.WriteAllText("deliverymen.json", JsonSerializer.Serialize(Pizzeria.DeliveryMen, App.jsonOptions));
        }

        public static void LoadMenu()
        {
            Pizzeria.PizzasMenu = new();
            Pizzeria.DrinksMenu = new();

            if (File.Exists("menu_pizzas.json"))
                Pizzeria.PizzasMenu = JsonSerializer.Deserialize<List<Pizza>>(File.ReadAllText("menu_pizzas.json"), App.jsonOptions);
            if (File.Exists("menu_drinks.json"))
                Pizzeria.DrinksMenu= JsonSerializer.Deserialize<List<Drink>>(File.ReadAllText("menu_drinks.json"), App.jsonOptions);
        }

        public static void SaveMenu()
        {
            File.WriteAllText("menu_pizzas.json", JsonSerializer.Serialize(Pizzeria.PizzasMenu, App.jsonOptions));
            File.WriteAllText("menu_drinks.json", JsonSerializer.Serialize(Pizzeria.DrinksMenu, App.jsonOptions));
        }
    }
}
