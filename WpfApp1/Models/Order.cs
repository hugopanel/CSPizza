using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Order
    {
        private static int GlobalIdCount = 0;

        /// <summary>
        /// The Identification number of the order.
        /// It cannot be set manually. It increments automatically each time the class is instanciated.
        /// </summary>
        public int Id { get; } = GlobalIdCount++;

        /// <summary>
        /// The status of the order, as defined by OrderStatus.
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Taking;

        /// <summary>
        /// Date and time of when the command was taken. 
        /// </summary>
        public DateTime dateTime { get; }

        /// <summary>
        /// The list of items in the order.
        /// </summary>
        public List<Item> Items { get; set; } = new();
        
        /// <summary>
        /// The Clerk in charge of the command.
        /// </summary>
        public Clerk Clerk { get; init; }

        /// <summary>
        /// Le Customer qui a fait la commande.
        /// </summary>
        public Customer Customer { get; init; }

        public Order(Clerk clerk) 
        {
            Clerk = clerk;
        }

        // Probably shouldn't ever use the following two constructors, but they're here just in case, for now...
        public Order(Clerk clerk, OrderStatus status) : this(clerk)
        {
            Status = status;
        }

        public Order(Clerk clerk, OrderStatus status, List<Item> items) : this(clerk, status)
        {
            Items = items;
        }


        /// <summary>
        /// Add an item to the order.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items.Add(item);
            Customer.CumulativeAmount += item.Price;
        }

        /// <summary>
        /// Remove an item from the order.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            Customer.CumulativeAmount -= item.Price;
        }

        /// <summary>
        /// Confirms the order but DOES NOT send any message.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Confirm()
        {
            Status = OrderStatus.Taken;

            // Remarque : le message pour signaler que la commande a été validée est envoyée depuis une autre fonction. 
        }

        public float getPrice(){
            float price = 0;
            foreach (Item item in Items)
            {
                price+=item.Price;
            }
            return price;
        }

        public float addToCumulative(Customer customer, float amount)
        {
            if(Customer == customer)
            {
                customer.CumulativeAmount += this.getPrice();
            }
            return amount;
        }

        public int IsToClerk(Clerk clerk, int nbOrders)
        {
            if (this.Clerk == clerk)
            {
                nbOrders += 1;
            }
            return nbOrders;
        }
    }
}
