using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This class contains subscores for many of the individual components that are
    /// used to calculate the overall risk score.
    /// </summary>
    public class Subscores
    {
        /// <summary>
        /// The risk associated with the AVS result. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("avs_result")]
        public double? AvsResult { get; internal set; }

        /// <summary>
        /// The risk associated with the billing address. If present, this is
        /// a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("billing_address")]
        public double? BillingAddress { get; internal set; }

        /// <summary>
        /// The risk associated with the distance between the billing address
        /// and the location for the given IP address. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("billing_address_distance_to_ip_location")]
        public double? BillingAddressDistanceToIPLocation { get; internal set; }

        /// <summary>
        /// The risk associated with the browser attributes such as the
        /// User-Agent and Accept-Language. If present, this is a value in the
        /// range 0.01 to 99.
        /// </summary>
        [JsonProperty("browser")]
        public double? Browser { get; internal set; }

        /// <summary>
        /// Individualized risk of chargeback for the given IP address on
        /// your account and shop ID.This is only available to users sending
        /// chargeback data to MaxMind. If present, this is a value in the
        /// range 0.01 to 99.
        /// </summary>
        [JsonProperty("chargeback")]
        public double? Chargeback { get; internal set; }

        /// <summary>
        /// The risk associated with the country the transaction originated
        /// from. If present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("country")]
        public double? Country { get; internal set; }

        /// <summary>
        /// The risk associated with the combination of IP country, card
        /// issuer country, billing country, and shipping country. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("country_mismatch")]
        public double? CountryMismatch { get; internal set; }

        /// <summary>
        /// The risk associated with the CVV result. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("cvv_result")]
        public double? CvvResult { get; internal set; }

        /// <summary>
        /// The risk associated with the particular email address. If
        /// present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("email_address")]
        public double? EmailAddress { get; internal set; }

        /// <summary>
        /// The general risk associated with the email domain. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("email_domain")]
        public double? EmailDomain { get; internal set; }

        /// <summary>
        /// The risk associated with the issuer ID number on the email
        /// domain. If present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("email_tenure")]
        public double? EmailTenure { get; internal set; }

        /// <summary>
        /// The risk associated with the issuer ID number on the IP address.
        /// If present, this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("ip_tenure")]
        public double? IPTenure { get; internal set; }

        /// <summary>
        /// The risk associated with the particular issuer ID number (IIN)
        /// given the billing location and the history of usage of the IIN on
        /// your account and shop ID. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonProperty("issuer_id_number")]
        public double? IssuerIdNumber { get; internal set; }

        /// <summary>
        /// The risk associated with the particular order amount for your
        /// account and shop ID. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonProperty("order_amount")]
        public double? OrderAmount { get; internal set; }

        /// <summary>
        /// The risk associated with the particular phone number. If present,
        /// this is a value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("phone_number")]
        public double? PhoneNumber { get; internal set; }

        /// <summary>
        /// The risk associated with the distance between the shipping address
        /// and the location for the given IP address. If present, this is a
        /// value in the range 0.01 to 99.
        /// </summary>
        [JsonProperty("shipping_address_distance_to_ip_location")]
        public double? ShippingAddressDistanceToIPLocation { get; internal set; }

        /// <summary>
        /// The risk associated with the local time of day of the transaction
        /// in the IP address location. If present, this is a value in the range
        /// 0.01 to 99.
        /// </summary>
        [JsonProperty("time_of_day")]
        public double? TimeOfDay { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"AvsResult: {AvsResult}, BillingAddress: {BillingAddress}, BillingAddressDistanceToIPLocation: {BillingAddressDistanceToIPLocation}, Browser: {Browser}, Chargeback: {Chargeback}, Country: {Country}, CountryMismatch: {CountryMismatch}, CvvResult: {CvvResult}, EmailAddress: {EmailAddress}, EmailDomain: {EmailDomain}, EmailTenure: {EmailTenure}, IPTenure: {IPTenure}, IssuerIdMumber: {IssuerIdNumber}, OrderAmount: {OrderAmount}, PhoneNumber: {PhoneNumber}, ShippingAddressDistanceToIPLocation: {ShippingAddressDistanceToIPLocation}, TimeOfDay: {TimeOfDay}";
        }
    }
}
