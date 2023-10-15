namespace WpfApp1.Models
{
    internal class Drink : Item
    {
        public Drink(string name, float price)
        {
            Name = name;
            Price = price;
            PreparationTime = 0;
        }
    }
}
