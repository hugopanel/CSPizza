﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal static class Pizzeria
    {
        /// <summary>
        /// A list of all the valid addresses the pizzeria can deliver to. 
        /// </summary>
        public static List<Address> GPS { get; set; } = new() { 
            new Address("30-32 Av. de la République", "Villejuif", 10000),
            new Address("136 bis Bd Maxime Gorki", "Villejuif", 9000),
            new Address("Genshin Street","Kuala Lumpur", 1200000),
            new Address("Minoring Avenue","Paris", 1500000),
            new Address("Karmine Boulevard","Saint-Michel-Chef-Chef", 1500000),
            new Address("3 rue Minoring", "Paris", 3000)
        };

        public static List<Customer> Customers { get; set; } = new();
        public static List<Clerk> Clerks { get; set; } = new();
        public static List<Cook> Cooks { get; set; } = new();
        public static List<DeliveryMan> DeliveryMen { get; set; } = new();
        public static List<Pizza> PizzasMenu { get; set; } = new();
        public static List<Drink> DrinksMenu { get; set; } = new();

        public static List<Order> Orders {  get; set; } = new();
    }
}
