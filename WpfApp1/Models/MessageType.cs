using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public enum MessageType
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
        /// The Customer tells the Clerk what their Order is.
        /// </summary>
        SubmitOrder,

        /// <summary>
        /// The Clerk tells the Customer that their Order has been confirmed.
        /// In reponse to SubmitOrder.
        /// </summary>
        ConfirmationSubmitOrder,

        /// <summary>
        /// The Clerk tells the Cooks that a new Order is pending.
        /// If the Clerk receives no KitchenPreparingOrder message, they retry after a few seconds.
        /// </summary>
        KitchenNewOrder,

        /// <summary>
        /// The Cook tells the Clerk that they started preparing the order.
        /// </summary>
        KitchenPreparingOrder,

        /// <summary>
        /// The Clerk tells the Customer that their Order is being prepared by the Cooks.
        /// </summary>
        ClerkOrderPreparing,

        /// <summary>
        /// The Cooks tell the Clerk that an Order is ready to be delivered.
        /// </summary>
        KitchenOrderReady,

        /// <summary>
        /// The Clerk tells the Customer that their Order is ready to be delivered and is waiting for a
        /// DeliveryMan to be free.
        /// </summary>
        ClerkOrderWaiting,

        /// <summary>
        /// The Clerk tells the DeliveryMan that an Order is waiting to be delivered.
        /// If the Clerk receives no DeliveryGoing message, they retry after a few seconds.
        /// </summary>
        DeliveryNewOrder,

        /// <summary>
        /// The DeliveryMan tells the Clerk that they are departing from the Pizzeria to deliver an Order.
        /// </summary>
        DeliveryGoing,

        /// <summary>
        /// The Clerk tells the Customer that their Order is being delivered.
        /// </summary>
        ClerkOrderDelivering,

        /// <summary>
        /// The DeliveryMan tells the Clerk that they dropped the Order.
        /// </summary>
        DeliveryDropped,

        /// <summary>
        /// The Clerk tells the Customer that their Order has been dropped.
        /// </summary>
        ClerkOrderDropped,

        /// <summary>
        /// The DeliveryMan tells the Clerk that an Order has been successfully delivered.
        /// </summary>
        DeliveryArrivedToCustomer,

        /// <summary>
        /// The Clerk tells the Customer that their order has been delivered to them.
        /// </summary>
        ClerkOrderDelivered,

        /// <summary>
        /// The DeliveryMan tells the Clerk that they came back to the pizzeria.
        /// </summary>
        DeliveryArrivedAtPizzeria,

        /// <summary>
        /// The Clerk tells the Customer that the DeliveryMan has returned and that they have received the
        /// money.
        /// </summary>
        ClerkOrderDone
    }
}