using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Information about the credit card based on the issuer ID number.
    /// </summary>
    public class CreditCard
    {
        /// <summary>
        /// An object containing information about the credit card issuer.
        /// </summary>
        [JsonProperty("issuer")]
        public Issuer Issuer { get; internal set; } = new Issuer();

        /// <summary>
        /// This property contains an <see href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">
        /// ISO 3166-1 alpha-2</see> country code for the country that the card
        /// was issued in.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        /// This property is <c>true</c> if the country of the billing 
        /// address matches the country that the credit card was issued in.
        /// </summary>
        [JsonProperty("is_issued_in_billing_address_country")]
        public bool? IsIssuedInBillingAddressCountry { get; internal set; }

        /// <summary>
        /// This property is <c>true</c> if the card is a prepaid card.
        /// </summary>
        [JsonProperty("is_prepaid")]
        public bool? IsPrepaid { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"Issuer: {{{Issuer}}}, Country: {Country}, IsIssuedInBillingAddressCountry: {IsIssuedInBillingAddressCountry}, IsPrepaid: {IsPrepaid}";
        }
    }
}