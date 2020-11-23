﻿using System.Text.Json.Serialization;

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
        [JsonInclude]
        [JsonPropertyName("ip_address")]
        public new IPAddress IPAddress { get; init; } = new IPAddress();

        /// <summary>
        /// An object containing minFraud data about the credit card used in
        /// the transaction.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("credit_card")]
        public CreditCard CreditCard { get; init; } = new CreditCard();

        /// <summary>
        /// This object contains information about the device that MaxMind
        /// believes is associated with the IP address passed in the request.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("device")]
        public Device Device { get; init; } = new Device();

        /// <summary>
        /// This object contains information about the email address passed in
        ///  the request.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("email")]
        public Email Email { get; init; } = new Email();

        /// <summary>
        /// An object containing minFraud data related to the shipping address
        ///  used in the transaction.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("shipping_address")]
        public ShippingAddress ShippingAddress { get; init; } = new ShippingAddress();

        /// <summary>
        /// An object containing minFraud data related to the billing address
        /// used in the transaction.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("billing_address")]
        public BillingAddress BillingAddress { get; init; } = new BillingAddress();

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