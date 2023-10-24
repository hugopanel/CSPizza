using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal enum MessageType
    {
        /// <summary>
        /// The Customer is calling the pizzeria.
        /// </summary>
        InitialCall,
        /// <summary>
        /// The Clerk is asking if it is the Customer's first time ordering.
        /// </summary>
        AskFirstOrder,
        /// <summary>
        /// The Customer tells the Clerk if it is their first time ordering.
        /// In response to AskFirstOrder.
        /// </summary>
        AnswerFirstOrder,
        /// <summary>
        /// The Clerk asks the Customer for their phone number.
        /// </summary>
        AskPhoneNumber,
        /// <summary>
        /// The Customer tells their phone number.
        /// In response to AskPhoneNumber.
        /// </summary>
        AnswerPhoneNumber,
        /// <summary>
        /// The Clerk asks for the complete information of the Customer.
        /// </summary>
        AskInfo,
        /// <summary>
        /// The Customer sends (in a single message) their information.
        /// In response to AskInfo.
        /// </summary>
        AnswerInfo,
        /// <summary>
        /// The Clerk asks the Customer if the information they have is correct.
        /// </summary>
        AskIsInfoCorrect,
        /// <summary>
        /// The Customer tells the Clerk if the information they have is correct.
        /// In response to AskIsInfoCorrect.
        /// </summary>
        AnswerIsInfoCorrect,
        /// <summary>
        /// The Customer adds an item to their order.
        /// </summary>
        AddToOrder,
        /// <summary>
        /// The Clerk tells the Customer that the item has succefully been added to the Order.
        /// In response to AddToCommand.
        /// </summary>
        ConfirmationAddToOrder,
        /// <summary>
        /// The Customer removes an item from their order, from an id.
        /// </summary>
        RemoveFromOrder,
        /// <summary>
        /// The Clerk tells the Customer that the item has been removed.
        /// In response to RemoveFromCommand.
        /// </summary>
        ConfirmationRemoveFromOrder,
        /// <summary>
        /// The Customer asks the Clerk to give them a list of their Order.
        /// </summary>
        AskDisplayFullOrder,
        /// <summary>
        /// The Clerk tells the Customer what their Order contains.
        /// In response to AskDisplayFullCommand.
        /// </summary>
        AnswerDisplayFullOrder,
        /// <summary>
        /// The Customer asks to confirm their Order.
        /// </summary>
        ConfirmOrder,
        /// <summary>
        /// The Clerk tells the Customer that their Order has been confirmed.
        /// In reponse to ConfirmCommand.
        /// </summary>
        ConfirmationConfirmOrder,
        /// <summary>
        /// The Clerk tells the Cooks that a new Order is pending.
        /// </summary>
        KitchenNewOrder,
        /// <summary>
        /// The Cooks tell the Clerk that an Order is ready to be delivered.
        /// </summary>
        KitchenOrderReady,
        /// <summary>
        /// The Clerk tells the DeliveryMan that an Order is waiting to be delivered.
        /// </summary>
        DeliveryNewOrder,
        /// <summary>
        /// The DeliveryMan tells the Clerk that they are departing from the Pizzeria to deliver an Order.
        /// </summary>
        DeliveryGoing,
        /// <summary>
        /// The DeliveryMan tells the Clerk that they dropped the Order.
        /// </summary>
        DeliveryDropped,
        /// <summary>
        /// The DeliveryMan tells the Clerk that an Order has been successfully delivered.
        /// </summary>
        DeliveryArrivedToCustomer,
        /// <summary>
        /// The DeliveryMan tells the Clerk that they came back to the pizzeria.
        /// </summary>
        DeliveryArrivedAtPizzeria,
    }
}