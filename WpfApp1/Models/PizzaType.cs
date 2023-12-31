﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class PizzaType
    {
        /// <summary>
        /// The name of the pizza's type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The base price for this type of pizza.
        /// </summary>
        public float BasePrice { get; set; }

        public PizzaType(string name, float baseprice) { 
            Name = name;
            BasePrice = baseprice;
        }
    }
}
