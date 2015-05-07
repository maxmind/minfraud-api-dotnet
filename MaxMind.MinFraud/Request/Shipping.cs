using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public enum ShippingDeliverySpeed
    {
        [EnumMember(Value = "same_day")] SameDay,
        [EnumMember(Value = "overnight")] Overnight,
        [EnumMember(Value = "expedited")] Expedited,
        [EnumMember(Value = "standard")] Standard
    }

    public class Shipping : Location
    {
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

        [JsonProperty("delivery_speed")]
        public ShippingDeliverySpeed? DeliverySpeed { get; }

        public override string ToString()
        {
            return $"{base.ToString()}, DeliverySpeed: {DeliverySpeed}";
        }
    }
}