using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// Enumeration of shipping speeds supported by minFraud.
    /// </summary>
    public enum ShippingDeliverySpeed
    {
#pragma warning disable CS1591
        [EnumMember(Value = "same_day")] SameDay,
        [EnumMember(Value = "overnight")] Overnight,
        [EnumMember(Value = "expedited")] Expedited,
        [EnumMember(Value = "standard")] Standard
#pragma warning restore
    }

    /// <summary>
    /// The shipping information for the transaction being sent to the
    /// web service.
    /// </summary>
    public class Shipping : Location
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName">The first name of the end user as provided in their shipping information.</param>
        /// <param name="lastName">The last name of the end user as provided in their shipping information.</param>
        /// <param name="company">The company of the end user as provided in their shipping information.</param>
        /// <param name="address">The first line of the user’s shipping address.</param>
        /// <param name="address2">The second line of the user’s shipping address.</param>
        /// <param name="city">The city of the user’s shipping address.</param>
        /// <param name="region">The <a href="http://en.wikipedia.org/wiki/ISO_3166-2">ISO 3166-2</a>
        /// subdivision code for the user’s shipping address.</param>
        /// <param name="country">The two character <a href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO
        /// 3166-1 alpha-2</a> country code of the user’s shipping address.</param>
        /// <param name="postal">The postal code of the user’s shipping address.</param>
        /// <param name="phoneNumber">The phone number without the country code for the user’s shipping address.</param>
        /// <param name="phoneCountryCode">The country code for phone number associated with the user’s shipping address.</param>
        /// <param name="deliverySpeed">The shipping delivery speed for the order.</param>
        public Shipping(
            string firstName = null,
            string lastName = null,
            string company = null,
            string address = null,
            string address2 = null,
            string city = null,
            string region = null,
            string country = null,
            string postal = null,
            string phoneNumber = null,
            string phoneCountryCode = null,
            ShippingDeliverySpeed? deliverySpeed = null
            ) : base(
                firstName: firstName,
                lastName: lastName,
                company: company,
                address: address,
                address2: address2,
                city: city,
                region: region,
                country: country,
                postal: postal,
                phoneNumber: phoneNumber,
                phoneCountryCode: phoneCountryCode
                )
        {
            DeliverySpeed = deliverySpeed;
        }

        /// <summary>
        /// The shipping delivery speed for the order.
        /// </summary>
        [JsonProperty("delivery_speed")]
        public ShippingDeliverySpeed? DeliverySpeed { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{base.ToString()}, DeliverySpeed: {DeliverySpeed}";
        }
    }
}
