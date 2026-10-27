using MaxMind.MinFraud.Util;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The enumerated methods for verifying the phone number.
    /// </summary>
    public enum LocationPhoneVerificationMethod
    {
        /// <summary>
        /// A code delivered to the phone, such as by SMS, voice call, or messaging app.
        /// </summary>
        [EnumMember(Value = "delivered_code")]
        DeliveredCode,

        /// <summary>
        /// Verification through the mobile network operator, such as silent network
        /// authentication.
        /// </summary>
        [EnumMember(Value = "network")]
        Network,

        /// <summary>
        /// Another verification method.
        /// </summary>
        [EnumMember(Value = "other")]
        Other
    }

    /// <summary>
    /// The location information for the transaction being sent to the
    /// web service.
    /// </summary>
    public abstract record Location
    {
        private static readonly Regex CountryRe = new("^[A-Z]{2}$", RegexOptions.Compiled);

        /// <summary>
        /// Constructor.
        /// </summary>
        protected Location() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName">The first name of the end user as provided in their address information.</param>
        /// <param name="lastName">The last name of the end user as provided in their address information.</param>
        /// <param name="company">The company of the end user as provided in their address information.</param>
        /// <param name="address">The first line of the user’s address.</param>
        /// <param name="address2">The second line of the user’s address.</param>
        /// <param name="city">The city of the user’s address.</param>
        /// <param name="region">The <a href="https://en.wikipedia.org/wiki/ISO_3166-2">ISO 3166-2</a>
        /// subdivision code for the user’s address.</param>
        /// <param name="country">The two character <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO
        /// 3166-1 alpha-2</a> country code of the user’s address.</param>
        /// <param name="postal">The postal code of the user’s address.</param>
        /// <param name="phoneNumber">The phone number without the country code for the user’s address.</param>
        /// <param name="phoneCountryCode">The country code for phone number associated with the user’s address.</param>
        [Obsolete("Use object initializer syntax.")]
        protected Location(
            string? firstName = null,
            string? lastName = null,
            string? company = null,
            string? address = null,
            string? address2 = null,
            string? city = null,
            string? region = null,
            string? country = null,
            string? postal = null,
            string? phoneNumber = null,
            string? phoneCountryCode = null
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
        [JsonPropertyName("first_name")]
        public string? FirstName { get; init; }

        /// <summary>
        /// The last name associated with the address.
        /// </summary>
        [JsonPropertyName("last_name")]
        public string? LastName { get; init; }

        /// <summary>
        /// The company name associated with the address.
        /// </summary>
        [JsonPropertyName("company")]
        public string? Company { get; init; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; init; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        [JsonPropertyName("address_2")]
        public string? Address2 { get; init; }

        /// <summary>
        /// The city associated with the address.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; init; }

        /// <summary>
        /// The ISO 3166-2 subdivision code for the region associated
        /// with the address.
        /// </summary>
        [JsonPropertyName("region")]
        public string? Region { get; init; }

        /// <summary>
        /// The ISO 3166-1 alpha-2 country code for the country
        /// associated with the address (e.g., "US")
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country
        {
            get => field;
            init
            {
                if (value != null && !CountryRe.IsMatch(value))
                {
                    throw new ArgumentException("Expected two-letter country code in the ISO 3166-1 alpha-2 format");
                }
                field = value;
            }
        }

        /// <summary>
        /// The postal code for associated with the address.
        /// </summary>
        [JsonPropertyName("postal")]
        public string? Postal { get; init; }

        /// <summary>
        /// The phone number, without the country code, associated with the address.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; init; }

        /// <summary>
        /// The phone country code for the phone number associated with the address.
        /// </summary>
        [JsonPropertyName("phone_country_code")]
        public string? PhoneCountryCode { get; init; }

        /// <summary>
        /// The most recent method used to verify the phone number.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<LocationPhoneVerificationMethod>))]
        [JsonPropertyName("phone_verification_method")]
        public LocationPhoneVerificationMethod? PhoneVerificationMethod { get; init; }

        /// <summary>
        /// Whether the most recent verification of the phone number
        /// succeeded. Do not set this if no verification was attempted.
        /// </summary>
        [JsonPropertyName("phone_was_verification_successful")]
        public bool? PhoneWasVerificationSuccessful { get; init; }

        /// <summary>
        /// The date and time of the most recent verification of the
        /// phone number. The time must not be before 1970-01-01T00:00:00Z
        /// or more than two days in the future.
        /// A <c>DateTime</c> with Kind <c>Unspecified</c> is treated as local
        /// time. For a UTC value, use
        /// <c>DateTime.SpecifyKind(value, DateTimeKind.Utc)</c> or
        /// <c>new DateTimeOffset(value, TimeSpan.Zero)</c>.
        /// </summary>
        [JsonPropertyName("phone_verification_time")]
        public DateTimeOffset? PhoneVerificationTime { get; init; }
    }
}
