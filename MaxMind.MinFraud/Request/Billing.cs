using MaxMind.MinFraud.Util;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The enumerated methods for verifying the billing phone number.
    /// </summary>
    public enum PhoneVerificationMethod
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        [EnumMember(Value = "delivered_code")]
        DeliveredCode,

        [EnumMember(Value = "network")]
        Network,

        [EnumMember(Value = "other")]
        Other
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

    /// <summary>
    /// The billing information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed record Billing : Location
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Billing() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="firstName">The first name of the end user as provided in their billing information.</param>
        /// <param name="lastName">The last name of the end user as provided in their billing information.</param>
        /// <param name="company">The company of the end user as provided in their billing information.</param>
        /// <param name="address">The first line of the user’s billing address.</param>
        /// <param name="address2">The second line of the user’s billing address.</param>
        /// <param name="city">The city of the user’s billing address.</param>
        /// <param name="region">The <a href="https://en.wikipedia.org/wiki/ISO_3166-2">ISO 3166-2</a>
        /// subdivision code for the user’s billing address.</param>
        /// <param name="country">The two character <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">ISO
        /// 3166-1 alpha-2</a> country code of the user’s billing address.</param>
        /// <param name="postal">The postal code of the user’s billing address.</param>
        /// <param name="phoneNumber">The phone number without the country code for the user’s billing address.</param>
        /// <param name="phoneCountryCode">The country code for phone number associated with the user’s billing address.</param>
        [Obsolete("Use object initializer syntax.")]
        public Billing(
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
        }

        /// <summary>
        /// The most recent method used to verify the billing phone number.
        /// </summary>
        [JsonConverter(typeof(EnumMemberValueConverter<PhoneVerificationMethod>))]
        [JsonPropertyName("phone_verification_method")]
        public PhoneVerificationMethod? PhoneVerificationMethod { get; init; }

        /// <summary>
        /// Whether the most recent verification of the billing phone number
        /// succeeded. Do not set this if no verification was attempted.
        /// </summary>
        [JsonPropertyName("phone_was_verification_successful")]
        public bool? PhoneWasVerificationSuccessful { get; init; }

        /// <summary>
        /// The date and time of the most recent verification of the billing
        /// phone number.
        /// </summary>
        [JsonPropertyName("phone_verification_time")]
        public DateTimeOffset? PhoneVerificationTime { get; init; }
    }
}
