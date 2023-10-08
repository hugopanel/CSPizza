using System;
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
            new Address("30-32 Av. de la République, Villejuif", 10000),
            new Address("136 bis Bd Maxime Gorki, Villejuif", 9000)
        };
    }
}
