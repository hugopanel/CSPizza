using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class Address
    {
        /// <summary>
        /// Street name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Time it takes to deliver an order to that address (in milliseconds).
        /// </summary>
        public int DeliveryTime { get; set; }
    }
}
