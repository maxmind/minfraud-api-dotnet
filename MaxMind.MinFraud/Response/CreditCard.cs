using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Information about the credit card based on the issuer ID number.
    /// </summary>
    public sealed class CreditCard
    {
        /// <summary>
        /// The credit card brand.
        /// </summary>
        [JsonPropertyName("brand")]
        public string? Brand { get; init; }

        /// <summary>
        /// The two letter <a href="https://en.wikipedia.org/wiki/ISO_3166-1_alpha-2">
        /// ISO 3166-1 alpha-2</a> country code associated with the location
        /// of the majority of customers using this credit card as determined
        /// by their billing address. In cases where the location of customers
        /// is highly mixed, this defaults to the country of the bank issuing
        /// the card.
        /// </summary>
        [JsonPropertyName("country")]
        public string? Country { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the card is a business card.
        /// </summary>
        [JsonPropertyName("is_business")]
        public bool? IsBusiness { get; init; }

        /// <summary>
        /// This field is <c>true</c> if the country of the billing address
        /// matches the country of the majority of customers using that IIN.
        /// In cases where the location of customers is highly mixed, the
        /// match is to the country of the bank issuing the card.
        /// </summary>
        [JsonPropertyName("is_issued_in_billing_address_country")]
        public bool? IsIssuedInBillingAddressCountry { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the card is a prepaid card.
        /// </summary>
        [JsonPropertyName("is_prepaid")]
        public bool? IsPrepaid { get; init; }

        /// <summary>
        /// This property is <c>true</c> if the card is a virtual card.
        /// </summary>
        [JsonPropertyName("is_virtual")]
        public bool? IsVirtual { get; init; }

        /// <summary>
        /// An object containing information about the credit card issuer.
        /// </summary>
        [JsonPropertyName("issuer")]
        public Issuer Issuer { get; init; } = new Issuer();

        /// <summary>
        /// The credit card type.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{nameof(Brand)}: {Brand}, {nameof(Country)}: {Country}, {nameof(IsBusiness)}: {IsBusiness}, {nameof(IsIssuedInBillingAddressCountry)}: {IsIssuedInBillingAddressCountry}, {nameof(IsPrepaid)}: {IsPrepaid}, {nameof(IsVirtual)}: {IsVirtual}, {nameof(Issuer)}: {Issuer}, {nameof(Type)}: {Type}";
        }
    }
}
