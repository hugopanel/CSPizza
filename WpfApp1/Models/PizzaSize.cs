using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class PizzaSize
    {
        /// <summary>
        /// Name of the size (small, medium...).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base price of the size (can be increased depending on the type).
        /// </summary>
        public float BasePrice { get; set; }

        public PizzaSize(string name, float baseprice)
        {
            Name = name;
            BasePrice = baseprice;
        }
    }
}
