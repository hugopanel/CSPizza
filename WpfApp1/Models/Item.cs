namespace WpfApp1.Models
{
    internal abstract class Item
    {
        /// <summary>
        /// Name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of the item (in euros)
        /// </summary>
        public float Price { get; set; }

        /// <summary>
        /// Time it takes for the cook to prepare the item (in milliseconds)
        /// </summary>
        public int PreparationTime { get; set; }
    }
}
