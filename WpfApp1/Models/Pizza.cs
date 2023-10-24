using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Pizza : Item
    {
        /// <summary>
        /// The type of the pizza.
        /// </summary>
        public PizzaType PizzaType { get; set; }

        /// <summary>
        /// The size of the pizza.
        /// </summary>
        public PizzaSize PizzaSize { get; set; }

        /// <summary>
        /// Represents a pizza item.
        /// Inherits from the Item class.
        /// </summary>
        /// <param name="name">The name of the pizza.</param>
        /// <param name="preparationTime">The time it takes for a cook to prepare the pizza.</param>
        /// <param name="pizzaType">The type of the pizza.</param>
        /// <param name="pizzaSize">The size of the pizza.</param>
        [JsonConstructor]
        public Pizza(string name, int preparationTime, PizzaType pizzaType, PizzaSize pizzaSize) {
            Name = name;
            PreparationTime = preparationTime;
            PizzaType = pizzaType;
            PizzaSize = pizzaSize;
            Price = PizzaType.BasePrice + PizzaSize.BasePrice;
        }

        /// <summary>
        /// Returns a string representation of the pizza.
        /// </summary>
        /// <returns>A string representing the pizza.</returns>
        public override string ToString()
        {
            return $"Pizza {Name}, Type {PizzaType}, Size {PizzaSize}, Price {Price}, Preparation time {PreparationTime}";
        }
    }
}
