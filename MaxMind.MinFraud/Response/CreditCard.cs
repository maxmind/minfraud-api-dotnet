using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class CreditCard
    {
        [JsonProperty("issuer")]
        public Issuer Issuer { get; internal set; } = new Issuer();

        [JsonProperty("country")]
        public string Country { get; internal set; }

        [JsonProperty("is_issued_in_billing_address_country")]
        public bool? IsIssuedInBillingAddressCountry { get; internal set; }

        [JsonProperty("is_prepaid")]
        public bool? IsPrepaid { get; internal set; }

        public override string ToString()
        {
            return
                $"Issuer: {{{Issuer}}}, Country: {Country}, IsIssuedInBillingAddressCountry: {IsIssuedInBillingAddressCountry}, IsPrepaid: {IsPrepaid}";
        }
    }
}