using Newtonsoft.Json;
using System.Collections.Generic;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    ///     The transaction to be sent to the web service.
    /// </summary>
    public sealed class Transaction
    {
        /// <summary>
        ///     Constructor. See
        ///     <a href="https://dev.maxmind.com/minfraud/#Request_Body">
        ///         the minFraud documentation
        ///     </a>
        ///     for a general overview of the request sent to the web
        ///     service.
        /// </summary>
        /// <param name="device">Information about the device used in the transaction. This param is required.</param>
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
            Device device,
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
        [JsonProperty("account")]
        public Account? Account { get; }

        /// <summary>
        ///     Information about the account used in the transaction.
        /// </summary>
        [JsonProperty("billing")]
        public Billing? Billing { get; }

        /// <summary>
        ///     Information about the credit card used in the transaction.
        /// </summary>
        [JsonProperty("credit_card")]
        public CreditCard? CreditCard { get; }

        /// <summary>
        ///     Custom inputs as configured on your account portal.
        /// </summary>
        [JsonProperty("custom_inputs")]
        public CustomInputs? CustomInputs { get; }

        /// <summary>
        ///     Information about the device used in the transaction.
        /// </summary>
        [JsonProperty("device")]
        public Device Device { get; }

        /// <summary>
        ///     Information about the email used in the transaction.
        /// </summary>
        [JsonProperty("email")]
        public Email? Email { get; }

        /// <summary>
        ///     Details about the event such as the time.
        /// </summary>
        [JsonProperty("event")]
        public Event? Event { get; }

        /// <summary>
        ///     Details about the order.
        /// </summary>
        [JsonProperty("order")]
        public Order? Order { get; }

        /// <summary>
        ///     Information about the payment processing.
        /// </summary>
        [JsonProperty("payment")]
        public Payment? Payment { get; }

        /// <summary>
        ///     Shipping information used in the transaction.
        /// </summary>
        [JsonProperty("shipping")]
        public Shipping? Shipping { get; }

        /// <summary>
        ///     List of shopping items in the transaction.
        /// </summary>
        [JsonProperty("shopping_cart")]
        public IList<ShoppingCartItem>? ShoppingCart { get; }

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