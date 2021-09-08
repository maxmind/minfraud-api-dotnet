using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    ///     The transaction to be sent to the web service.
    /// </summary>
    public sealed class Transaction
    {
        /// <summary>
        ///     Constructor. See
        ///     <a href="https://dev.maxmind.com/minfraud/api-documentation/requests?lang=en">
        ///         the minFraud documentation
        ///     </a>
        ///     for a general overview of the request sent to the web
        ///     service.
        /// </summary>
        /// <param name="device">Information about the device used in the transaction.</param>
        /// <param name="account">Information about the account used in the transaction.</param>
        /// <param name="billing">Billing information used in the transaction.</param>
        /// <param name="creditCard">Information about the credit card used in the transaction.</param>
        /// <param name="customInputs">Custom inputs as configured on your account portal.</param>
        /// <param name="email">Information about the email used in the transaction.</param>
        /// <param name="userEvent">Details about the event such as the time.</param>
        /// <param name="order">Details about the order.</param>
        /// <param name="payment">Information about the payment processing.</param>
        /// <param name="shipping">Shipping information used in the transaction.</param>
        /// <param name="shoppingCart">List of shopping items in the transaction.</param>
        public Transaction(
            Device? device = null,
            Account? account = null,
            Billing? billing = null,
            CreditCard? creditCard = null,
            CustomInputs? customInputs = null,
            Email? email = null,
            Event? userEvent = null,
            Order? order = null,
            Payment? payment = null,
            Shipping? shipping = null,
            IList<ShoppingCartItem>? shoppingCart = default(List<ShoppingCartItem>)
        )
        {
            Device = device;
            Account = account;
            Billing = billing;
            CreditCard = creditCard;
            CustomInputs = customInputs;
            Email = email;
            Event = userEvent;
            Order = order;
            Payment = payment;
            Shipping = shipping;
            ShoppingCart = shoppingCart;
        }

        /// <summary>
        ///     Account information for the transaction.
        /// </summary>
        [JsonPropertyName("account")]
        public Account? Account { get; init; }

        /// <summary>
        ///     Information about the account used in the transaction.
        /// </summary>
        [JsonPropertyName("billing")]
        public Billing? Billing { get; init; }

        /// <summary>
        ///     Information about the credit card used in the transaction.
        /// </summary>
        [JsonPropertyName("credit_card")]
        public CreditCard? CreditCard { get; init; }

        /// <summary>
        ///     Custom inputs as configured on your account portal.
        /// </summary>
        [JsonPropertyName("custom_inputs")]
        public CustomInputs? CustomInputs { get; init; }

        /// <summary>
        ///     Information about the device used in the transaction.
        /// </summary>
        [JsonPropertyName("device")]
        public Device? Device { get; init; }

        /// <summary>
        ///     Information about the email used in the transaction.
        /// </summary>
        [JsonPropertyName("email")]
        public Email? Email { get; init; }

        /// <summary>
        ///     Details about the event such as the time.
        /// </summary>
        [JsonPropertyName("event")]
        public Event? Event { get; init; }

        /// <summary>
        ///     Details about the order.
        /// </summary>
        [JsonPropertyName("order")]
        public Order? Order { get; init; }

        /// <summary>
        ///     Information about the payment processing.
        /// </summary>
        [JsonPropertyName("payment")]
        public Payment? Payment { get; init; }

        /// <summary>
        ///     Shipping information used in the transaction.
        /// </summary>
        [JsonPropertyName("shipping")]
        public Shipping? Shipping { get; init; }

        /// <summary>
        ///     List of shopping items in the transaction.
        /// </summary>
        [JsonPropertyName("shopping_cart")]
        public IList<ShoppingCartItem>? ShoppingCart { get; init; }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"Account: {Account}, Billing: {Billing}, CreditCard: {CreditCard}, Device: {Device}, Email: {Email}, Event: {Event}, Order: {Order}, Payment: {Payment}, Shipping: {Shipping}, ShoppingCart: {ShoppingCart}";
        }
    }
}
