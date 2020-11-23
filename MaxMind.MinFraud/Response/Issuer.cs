using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model for the credit card issuer data from minFraud.
    /// </summary>
    public sealed class Issuer
    {
        /// <summary>
        /// The name of the bank which issued the credit card.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the name matches the name
        /// provided in the request for the card issuer. It is <c>false</c>
        /// if the name does not match. The property is <c>null</c> if
        /// either no name or no issuer ID number (IIN) was provided in the
        /// request or if MaxMind does not have a name associated with the IIN.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("matches_provided_name")]
        public bool? MatchesProvidedName { get; init; }

        /// <summary>
        /// The phone number of the bank which issued the credit card. In some
        /// cases the phone number we return may be out of date.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the phone number matches
        /// the number provided in the request for the card issuer. It is
        /// <c>false</c> if the number does not match. It is
        /// <c>null</c> if either no phone number or no issuer ID
        /// number(IIN) was provided in the request or if MaxMind does not
        /// have a phone number associated with the IIN.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("matches_provided_phone_number")]
        public bool? MatchesProvidedPhoneNumber { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"Name: {Name}, MatchesProvidedName: {MatchesProvidedName}, PhoneNumber: {PhoneNumber}, MatchesProvidedPhoneNumber: {MatchesProvidedPhoneNumber}";
        }
    }
}