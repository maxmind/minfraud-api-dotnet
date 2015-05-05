using System.Collections.Generic;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    // XXX - separate score and insights?
    public class MinFraudRequest
    {
        public MinFraudRequest(
            Device device,
            Account account = null,
            Billing billing = null,
            CreditCard creditCard = null,
            Email email = null,
            // XXX - think of better name. "event" is reserved
            Event eventRequest = null,
            Order order = null,
            Payment payment = null,
            Shipping shipping = null,
            List<ShoppingCartItem> shoppingCart = default(List<ShoppingCartItem>)
            )
        {
            Device = device;
            Account = account;
            Billing = billing;
            CreditCard = creditCard;
            Email = email;
            Event = eventRequest;
            Order = order;
            Payment = payment;
            Shipping = shipping;
            ShoppingCart = shoppingCart;
        }

        [JsonProperty("account")]
        public Account Account { get; }

        [JsonProperty("billing")]
        public Billing Billing { get; }

        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; }

        [JsonProperty("device")]
        public Device Device { get; }

        [JsonProperty("email")]
        public Email Email { get; }

        [JsonProperty("event")]
        public Event Event { get; }

        [JsonProperty("order")]
        public Order Order { get; }

        [JsonProperty("payment")]
        public Payment Payment { get; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; }

        [JsonProperty("shopping_cart")]
        public List<ShoppingCartItem> ShoppingCart { get; }

        public override string ToString()
        {
            return
                $"Account: {Account}, Billing: {Billing}, CreditCard: {CreditCard}, Device: {Device}, Email: {Email}, Event: {Event}, Order: {Order}, Payment: {Payment}, Shipping: {Shipping}, ShoppingCart: {ShoppingCart}";
        }
    }
}