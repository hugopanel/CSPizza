using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RibbitMQ;
using WpfApp1.View;
using static WpfApp1.App;

namespace WpfApp1.Models
{
    public class Cook : Worker
    {
        [JsonConstructor]
        public Cook(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override void CheckIn()
        {
            RibbitMq.Subscribe(MessageType.KitchenNewOrder, HandleKitchenNewOrder);
        }

        public override void CheckOut()
        {
            RibbitMq.Unsubscribe(MessageType.KitchenNewOrder, HandleKitchenNewOrder);
        }

        /// <summary>
        /// Cook the pizzas, and retrieve the drinks.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task CookItem(Item item)
        {
            HomeView.CookPrintText("Cooking " + item.Name + "...", this);
            await Task.Delay(item.PreparationTime);
            HomeView.CookPrintText("Done cooking " + item.Name + "!", this);
        }

        /// <summary>
        /// Handle new orders and cooks them.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task HandleKitchenNewOrder(IMessage<MessageType> message)
        {
            HomeView.CookPrintText("I received a new order. Let's cook!", this);
            RibbitMq.Unsubscribe(MessageType.KitchenNewOrder, HandleKitchenNewOrder); // We unsubscribe

            Order order = (Order) message.Content!;

            RibbitMq.Send(new Message()
            {
                MessageType = MessageType.KitchenPreparingOrder,
                Content = order.Id
            });

            // Actually cook the items
            foreach (var pizza in order.Pizzas)
            {
                await CookItem(pizza);
            }

            foreach (var drink in order.Drinks)
            {
                await CookItem(drink);
            }

            HomeView.CookPrintText("The order is ready!", this);

            // Cooking is finished!
            // We send a message to tell the Clerk
            RibbitMq.Send(new Message()
            {
                MessageType = MessageType.KitchenOrderReady,
                Content = order.Id
            });

            // And we subscribe to KitchenNewOrder to be able to cook again!
            RibbitMq.Subscribe(MessageType.KitchenNewOrder, HandleKitchenNewOrder);
            HomeView.CookPrintText("I'm ready to cook some more!", this);
        }
    }
}
