using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Order
    {
        private static int GlobalIdCount = 0;

        /// <summary>
        /// The Identification number of the order.
        /// Unless set manually, it increments automatically each time the class is instanciated.
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

        public float Price { get; set; }

        /// <summary>
        /// The list of items in the order.
        /// </summary>
        public List<Pizza> Pizzas { get; set; } = new();

        public List<Drink> Drinks { get; set; } = new();

        /// <summary>
        /// The Clerk in charge of the command.
        /// </summary>
        public Clerk? Clerk { get; set; }

        /// <summary>
        /// Le Customer qui a fait la commande.
        /// </summary>
        public Customer Customer { get; init; }

        [JsonConstructor]
        public Order(int id, OrderStatus Status, DateTime dateTime, float Price, List<Pizza> Pizzas, List<Drink> Drinks, Clerk Clerk, Customer customer)
        {
            this.Id = id;
            this.Status = Status;
            this.dateTime = dateTime;
            this.Price = Price;
            this.Pizzas = Pizzas;
            this.Drinks = Drinks;
            this.Clerk = Clerk;
            this.Customer = customer;
            Statistics stats = new Statistics();
            if (id > GlobalIdCount)
            {
                GlobalIdCount = id + 1;
            }
        }
      
        public Order(Clerk clerk) 
        {
            Clerk = clerk;
            clerk.NbOrders++;
        }

        public Order(Customer customer, List<Pizza> pizzas, List<Drink> drinks, OrderStatus status = OrderStatus.Taking, int? id = null, Clerk? clerk = null)
        {
            if (id == null)
                Id = GlobalIdCount++;
            else
            {
                Id = (int) id;
                GlobalIdCount += 1;
            }

            Clerk = clerk;
            Customer = customer;
            Pizzas = pizzas;
            Drinks = drinks;
            Status = status;
        }
        

        /// <summary>
        /// Add an item to the order.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
            Customer.CumulativeAmount += pizza.Price;
        }


        public void AddDrink(Drink drink)
        {
            Drinks.Add(drink);
            Customer.CumulativeAmount += drink.Price;
        }

        /// <summary>
        /// Remove an item from the order.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemovePizza(Pizza pizza)
        {
            Pizzas.Remove(pizza);
            Customer.CumulativeAmount -= pizza.Price;
        }

        public void RemoveDrink(Drink drink)
        {
            Drinks.Remove(drink);
            Customer.CumulativeAmount -= drink.Price;
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

        public float getPrice()
        {
            float price = 0;
            foreach (Pizza pizza in Pizzas)
            {
                price += pizza.Price;
            }
            foreach (Drink drink in Drinks)
            {
                price += drink.Price;
            }
            return price;
        }

        public float addToCumulative(Customer customer, float amount)
        {
            if (Customer == customer)
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