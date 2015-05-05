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
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Address = address;
            Address2 = address2;
            City = city;
            Region = region;
            Country = country;
            Postal = postal;
            PhoneNumber = phoneNumber;
            PhoneCountryCode = phoneCountryCode;
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