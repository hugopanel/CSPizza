using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Address
    {
        /// <summary>
        /// Street name (and number)
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Time it takes to deliver an order to that address (in milliseconds).
        /// </summary>
        public int DeliveryTime { get; set; }

        /// <summary>
        /// Instanciates a new address.
        /// </summary>
        /// <param name="name">The address as a string</param>
        /// <param name="deliveryTime">The time it takes to deliver to that address (in milliseconds)</param>
        public Address(string street, string city, int deliveryTime) {
            Street = street;
            City = city;
            DeliveryTime = deliveryTime;
        }

        public override string ToString()
        {
            return $"Street: {Street}, City: {City}, Delivery Time: {DeliveryTime}ms";
        }

        public bool Equals(string address)
        {
            var fields = address.Split(", ");
            return fields[0] == City && fields[1] == Street;
        }
    }
}
