using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Information about the shipping address.
    /// </summary>
    public sealed class ShippingAddress : Address
    {
        /// <summary>
        /// This property is <c>true</c> if the shipping address is in
        /// the IP country. The property is <c>false</c> when the address
        /// is not in the IP country. If the shipping address could not be
        /// parsed or was not provided or the IP address could not be
        /// geolocated, then the property is <c>null</c>.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("is_high_risk")]
        public bool? IsHighRisk { get; internal set; }

        /// <summary>
        /// The distance in kilometers from the shipping address to billing
        /// address.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("distance_to_billing_address")]
        public int? DistanceToBillingAddress { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, IsHighRisk: {IsHighRisk}, DistanceToBillingAddress: {DistanceToBillingAddress}";
        }
    }
}