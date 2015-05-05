using System.Text.RegularExpressions;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public abstract class Location
    {
        private string _country;

        [JsonProperty("first_name")]
        public string FirstName { get; protected set; }

        [JsonProperty("last_name")]
        public string LastName { get; protected set; }

        [JsonProperty("company")]
        public string Company { get; protected set; }

        [JsonProperty("address")]
        public string Address { get; protected set; }

        [JsonProperty("address_2")]
        public string Address2 { get; protected set; }

        [JsonProperty("city")]
        public string City { get; protected set; }

        [JsonProperty("region")]
        public string Region { get; protected set; }

        [JsonProperty("country")]
        public string Country
        {
            get { return _country; }
            protected set
            {
                if (value == null)
                {
                    return;
                }
                var re = new Regex("^[A-Z]{2}$");
                if (!re.IsMatch(value))
                {
                    throw new InvalidInputException("Expected two-letter country code in the ISO 3166-1 alpha-2 format");
                }
                _country = value;
            }
        }

        [JsonProperty("postal")]
        public string Postal { get; protected set; }

        [JsonProperty("phone_number")]
        public string PhoneNumber { get; protected set; }

        [JsonProperty("phone_country_code")]
        public string PhoneCountryCode { get; protected set; }

        public override string ToString()
        {
            return
                $"FirstName: {FirstName}, LastName: {LastName}, Company: {Company}, Address: {Address}, Address2: {Address2}, Region: {Region}, Country: {Country}, Postal: {Postal}, City: {City}, PhoneNumber: {PhoneNumber}, PhoneCountryCode: {PhoneCountryCode}";
        }
    }
}