﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Drink : Item
    {
        public string Size { get; set; }
        public Drink(string name, float price, string size) {
            Name = name;
            Price = price;
            Size = size;
            PreparationTime = 0;
        }

        public void UpdatePrice()
        {
            var typeDrink = Pizzeria.DrinksMenu.FirstOrDefault(item => item.Name == this.Name && item.Size == this.Size);
            Price = typeDrink.Price;
        }
    }
}
