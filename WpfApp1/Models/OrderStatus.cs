namespace WpfApp1.Models
{
    // TODO: Check naming!

    /// <summary>
    /// Defines every status an Order can have.
    /// </summary>
    internal enum OrderStatus
    {
        /// <summary>
        /// The orden has been taken and is waiting to be taken care of...
        /// </summary>
        Taken,
        /// <summary>
        /// The order is being prepared by the cooks.
        /// </summary>
        Preparing,
        /// <summary>
        /// The order has been prepared and is waiting to be delivered.
        /// </summary>
        Waiting,
        /// <summary>
        /// The order is being delivered.
        /// </summary>
        Delivering,
        /// <summary>
        /// The order has been delivered but the pizzeria hasn't been paid yet.
        /// </summary>
        Delivered,
        /// <summary>
        /// The order has been entirely fulfilled and the pizzeria has received the money.
        /// </summary>
        Done,
        /// <summary>
        /// The order has been canceled (by the customer or because it can't be delivered).
        /// </summary>
        Dropped
    }
}
