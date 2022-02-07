using System;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This class contains scores for many of the individual risk
    /// factors that are used to calculate the overall risk score.
    /// </summary>
    public sealed class Subscores
    {
        /// <summary>
        /// The risk associated with the AVS result. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("avs_result")]
        public double? AvsResult { get; init; }

        /// <summary>
        /// The risk associated with the billing address. If present, this is
        /// a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("billing_address")]
        public double? BillingAddress { get; init; }

        /// <summary>
        /// The risk associated with the distance between the billing address
        /// and the location for the given IP address. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("billing_address_distance_to_ip_location")]
        public double? BillingAddressDistanceToIPLocation { get; init; }

        /// <summary>
        /// The risk associated with the browser attributes such as the
        /// User-Agent and Accept-Language. If present, this is a value in the
        /// range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("browser")]
        public double? Browser { get; init; }

        /// <summary>
        /// Individualized risk of chargeback for the given IP address on
        /// your account and shop ID. This is only available to users sending
        /// chargeback data to MaxMind. If present, this is a value in the
        /// range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("chargeback")]
        public double? Chargeback { get; init; }

        /// <summary>
        /// The risk associated with the country the transaction originated
        /// from. If present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("country")]
        public double? Country { get; init; }

        /// <summary>
        /// The risk associated with the combination of IP country, card
        /// issuer country, billing country, and shipping country. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("country_mismatch")]
        public double? CountryMismatch { get; init; }

        /// <summary>
        /// The risk associated with the CVV result. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("cvv_result")]
        public double? CvvResult { get; init; }

        /// <summary>
        /// The risk associated with the device. If present, this is a value
        /// in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("device")]
        public double? Device { get; init; }

        /// <summary>
        /// The risk associated with the particular email address. If
        /// present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("email_address")]
        public double? EmailAddress { get; init; }

        /// <summary>
        /// The general risk associated with the email domain. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("email_domain")]
        public double? EmailDomain { get; init; }

        /// <summary>
        /// The risk associated with the email address local part (the part of
        /// the email address before the @ symbol). If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("email_local_part")]
        public double? EmailLocalPart { get; init; }

        /// <summary>
        /// The risk associated with the particular issuer ID number (IIN)
        /// given the billing location and the history of usage of the IIN on
        /// your account and shop ID. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonPropertyName("issuer_id_number")]
        public double? IssuerIdNumber { get; init; }

        /// <summary>
        /// The risk associated with the particular order amount for your
        /// account and shop ID. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonPropertyName("order_amount")]
        public double? OrderAmount { get; init; }

        /// <summary>
        /// The risk associated with the particular phone number. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public double? PhoneNumber { get; init; }

        /// <summary>
        /// The risk associated with the shipping address. If present, this is
        /// a value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("shipping_address")]
        public double? ShippingAddress { get; init; }

        /// <summary>
        /// The risk associated with the distance between the shipping address
        /// and the location for the given IP address. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonPropertyName("shipping_address_distance_to_ip_location")]
        public double? ShippingAddressDistanceToIPLocation { get; init; }

        /// <summary>
        /// The risk associated with the local time of day of the transaction
        /// in the IP address location. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonPropertyName("time_of_day")]
        public double? TimeOfDay { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"AvsResult: {AvsResult}, BillingAddress: {BillingAddress}, BillingAddressDistanceToIPLocation: {BillingAddressDistanceToIPLocation}, Browser: {Browser}, Chargeback: {Chargeback}, Country: {Country}, CountryMismatch: {CountryMismatch}, CvvResult: {CvvResult}, EmailAddress: {EmailAddress}, EmailDomain: {EmailDomain}, IssuerIdMumber: {IssuerIdNumber}, OrderAmount: {OrderAmount}, PhoneNumber: {PhoneNumber}, ShippingAddressDistanceToIPLocation: {ShippingAddressDistanceToIPLocation}, TimeOfDay: {TimeOfDay}";
        }
    }
}
