namespace WpfApp1.Models
{
    internal class PizzaSize
    {
        /// <summary>
        /// Name of the size (small, medium...).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base price of the size (can be increased depending on the type).
        /// </summary>
        public float BasePrice { get; set; }
    }
}
