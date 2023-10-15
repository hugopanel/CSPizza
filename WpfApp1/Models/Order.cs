using System.Collections.Generic;

namespace WpfApp1.Models
{
    internal class Order
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
        public OrderStatus Status { get; set; } = OrderStatus.Taken;

        /// <summary>
        /// The list of items in the order.
        /// </summary>
        public List<Item> Items { get; set; } = new();

        public Order() { }

        // Probably shouldn't ever use the following two constructors, but they're here just in case, for now...
        public Order(OrderStatus status)
        {
            Status = status;
        }

        public Order(OrderStatus status, List<Item> items)
        {
            Status = status;
            Items = items;
        }

        /// <summary>
        /// Add an item to the order.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Remove an item from the order.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
    }
}
