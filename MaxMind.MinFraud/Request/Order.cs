using System;
using System.Text.RegularExpressions;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class Order
    {
        private string _currency;

        public Order(
            decimal? amount = null,
            string currency = null,
            string discountCode = null,
            string affiliateId = null,
            string subaffiliateId = null,
            Uri referrerUri = null
            )
        {
            Amount = amount;
            Currency = currency;
            DiscountCode = discountCode;
            AffiliateId = affiliateId;
            SubaffiliateId = subaffiliateId;
            ReferrerUri = referrerUri;
        }

        [JsonProperty("amount")]
        public decimal? Amount { get; }

        [JsonProperty("currency")]
        public string Currency
        {
            get { return _currency; }
            private set
            {
                if (value == null)
                {
                    return;
                }
                var re = new Regex("^[A-Z]{3}$");
                if (!re.IsMatch(value))
                {
                    throw new InvalidInputException($"The currency code {value} is invalid.");
                }
                _currency = value;
            }
        }

        [JsonProperty("discount_code")]
        public string DiscountCode { get; }

        [JsonProperty("affiliate_id")]
        public string AffiliateId { get; }

        [JsonProperty("subaffiliate_id")]
        public string SubaffiliateId { get; }

        [JsonProperty("referrer_uri")]
        public Uri ReferrerUri { get; }

        public override string ToString()
        {
            return
                $"Amount: {Amount}, Currency: {Currency}, DiscountCode: {DiscountCode}, AffiliateId: {AffiliateId}, SubaffiliateId: {SubaffiliateId}, ReferrerUri: {ReferrerUri}";
        }
    }
}