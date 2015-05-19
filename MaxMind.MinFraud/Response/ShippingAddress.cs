using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class ShippingAddress : Address
    {
        [JsonProperty("is_high_risk")]
        public bool? IsHighRisk { get; internal set; }

        [JsonProperty("distance_to_billing_address")]
        public int? DistanceToBillingAddress { get; internal set; }

        public override string ToString()
        {
            return $"{base.ToString()}, IsHighRisk: {IsHighRisk}, DistanceToBillingAddress: {DistanceToBillingAddress}";
        }
    }
}