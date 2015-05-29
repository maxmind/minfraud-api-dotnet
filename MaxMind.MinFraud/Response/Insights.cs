using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for Insights response.
    /// </summary>
    public class Insights : Score
    {
        /// <summary>
        /// An object containing GeoIP2 and minFraud Insights information about
        /// the geo-located IP address.
        /// </summary>
        [JsonProperty("ip_location")]
        public IPLocation IPLocation { get; internal set; } = new IPLocation();

        /// <summary>
        /// An object containing minFraud data about the credit card used in 
        /// the transaction.
        /// </summary>
        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; internal set; } = new CreditCard();

        /// <summary>
        /// An object containing minFraud data related to the shipping address
        ///  used in the transaction.
        /// </summary>
        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress { get; internal set; } = new ShippingAddress();

        /// <summary>
        /// An object containing minFraud data related to the billing address
        /// used in the transaction.
        /// </summary>
        [JsonProperty("billing_address")]
        public BillingAddress BillingAddress { get; internal set; } = new BillingAddress();

        public override string ToString()
        {
            return
                $"{base.ToString()}, IPLocation: {{{IPLocation}}}, CreditCard: {{{CreditCard}}}, ShippingAddress: {{{ShippingAddress}}}, BillingAddress: {{{BillingAddress}}}";
        }
    }
}