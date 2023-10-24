using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Drink : Item
    {
        public Drink(string name, float price)
        {
            Name = name;
            Price = price;
            PreparationTime = 0;
        }
    }
}