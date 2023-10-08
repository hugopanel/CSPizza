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

        /// <summary>
        /// Instanciates a new address.
        /// </summary>
        /// <param name="name">The address as a string</param>
        /// <param name="deliveryTime">The time it takes to deliver to that address (in milliseconds)</param>
        public Address(string name, int deliveryTime) {
            Name = name;
            DeliveryTime = deliveryTime;
        }
    }
}
