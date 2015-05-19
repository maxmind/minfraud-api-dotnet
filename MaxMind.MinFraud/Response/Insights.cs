using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class Insights : Score
    {
        [JsonProperty("ip_location")]
        public IPLocation IPLocation { get; internal set; } = new IPLocation();

        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; internal set; } = new CreditCard();

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; internal set; } = new ShippingAddress();

        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; internal set; } = new BillingAddress();

        public override string ToString()
        {
            return
                $"{base.ToString()}, IPLocation: {{{IPLocation}}}, CreditCard: {{{CreditCard}}}, ShippingAddress: {{{ShippingAddress}}}, BillingAddress: {{{BillingAddress}}}";
        }
    }
}