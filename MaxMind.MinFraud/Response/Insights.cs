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
        /// the IP address.
        /// </summary>
        [JsonProperty("ip_address")]
        public new IPAddress IPAddress { get; internal set; } = new IPAddress();

        /// <summary>
        /// An object containing minFraud data about the credit card used in
        /// the transaction.
        /// </summary>
        [JsonProperty("credit_card")]
        public CreditCard CreditCard { get; internal set; } = new CreditCard();

        /// <summary>
        /// This object contains information about the device that MaxMind
        /// believes is associated with the IP address passed in the request.
        /// </summary>
        [JsonProperty("device")]
        public Device Device { get; internal set; } = new Device();

        /// <summary>
        /// This object contains information about the email address passed in
        ///  the request.
        /// </summary>
        [JsonProperty("email")]
        public Email Email { get; internal set; } = new Email();

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

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, IPAddress: {IPAddress}, CreditCard: {CreditCard}, Device: {Device}, Email: {Email}, ShippingAddress: {ShippingAddress}, BillingAddress: {BillingAddress}";
        }
    }
}