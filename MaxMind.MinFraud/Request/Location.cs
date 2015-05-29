using System;
using System.Text.RegularExpressions;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The location information for the transaction being sent to the
    /// web service.
    /// </summary>
    public abstract class Location
    {
        private string _country;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName">The first name of the end user as provided in their address information.</param>
        /// <param name="lastName">The last name of the end user as provided in their address information.</param>
        /// <param name="company">The company of the end user as provided in their address information.</param>
        /// <param name="address">The first line of the user’s address.</param>
        /// <param name="address2">The second line of the user’s address.</param>
        /// <param name="city">The city of the user’s address.</param>
        /// <param name="region">The <see href="http://en.wikipedia.org/wiki/ISO_3166-2">ISO 3166-2</see> 
        /// subdivision code for the user’s address.</param>
        /// <param name="country">The two character <see href="http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO
        /// 3166-1 alpha-2</see> country code of the user’s address.</param>
        /// <param name="postal">The postal code of the user’s address.</param>
        /// <param name="phoneNumber">The phone number without the country code for the user’s address.</param>
        /// <param name="phoneCountryCode">The country code for phone number associated with the user’s address.</param>
        protected Location(
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
            string phoneCountryCode = null
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
        }

        /// <summary>
        /// The first name associated with the address.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; protected set; }

        /// <summary>
        /// The last name associated with the address.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; protected set; }

        /// <summary>
        /// The company name associated with the address.
        /// </summary>
        [JsonProperty("company")]
        public string Company { get; protected set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; protected set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        [JsonProperty("address_2")]
        public string Address2 { get; protected set; }

        /// <summary>
        /// The city associated with the address.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; protected set; }

        /// <summary>
        /// The ISO 3166-2 subdivision code for the region associated 
        /// with the address.
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; protected set; }

        /// <summary>
        /// The ISO 3166-1 alpha-2 country code for the country
        /// associated with the address(e.g, "US")
        /// </summary>
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
                    throw new ArgumentException("Expected two-letter country code in the ISO 3166-1 alpha-2 format");
                }
                _country = value;
            }
        }

        /// <summary>
        /// The postal code for associated with the address.
        /// </summary>
        [JsonProperty("postal")]
        public string Postal { get; protected set; }

        /// <summary>
        /// The phone country code for the phone number associated with the address.
        /// </summary>
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; protected set; }

        /// <summary>
        /// The phone number, without the country code, associated with the address.
        /// </summary>
        [JsonProperty("phone_country_code")]
        public string PhoneCountryCode { get; protected set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"FirstName: {FirstName}, LastName: {LastName}, Company: {Company}, Address: {Address}, Address2: {Address2}, Region: {Region}, Country: {Country}, Postal: {Postal}, City: {City}, PhoneNumber: {PhoneNumber}, PhoneCountryCode: {PhoneCountryCode}";
        }
    }
}