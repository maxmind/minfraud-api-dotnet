using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The order information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Order
    {
        private string _currency;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="amount">The total order amount for the transaction.</param>
        /// <param name="currency">The ISO 4217 currency code for the currency
        /// used in the transaction.</param>
        /// <param name="discountCode">The discount code applied to the
        /// transaction. If multiple discount codes were used, please separate
        /// them with a comma.</param>
        /// <param name="affiliateId">The ID of the affiliate where the order is
        /// coming from.</param>
        /// <param name="subaffiliateId">The ID of the sub-affiliate where the
        /// order is coming from.</param>
        /// <param name="referrerUri">The URI of the referring site for this
        /// order.</param>
        /// <param name="isGift">Whether order was marked as a gift by the
        /// purchaser.</param>
        /// <param name="hasGiftMessage">Whether the purchaser included a gift
        /// message.</param>
        public Order(
            decimal? amount = null,
            string currency = null,
            string discountCode = null,
            string affiliateId = null,
            string subaffiliateId = null,
            Uri referrerUri = null,
            bool? isGift = null,
            bool? hasGiftMessage = null
            )
        {
            Amount = amount;
            Currency = currency;
            DiscountCode = discountCode;
            AffiliateId = affiliateId;
            SubaffiliateId = subaffiliateId;
            ReferrerUri = referrerUri;
            IsGift = isGift;
            HasGiftMessage = hasGiftMessage;
        }

        /// <summary>
        /// The total order amount for the transaction.
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Amount { get; }

        /// <summary>
        /// The ISO 4217 currency code for the currency used in the transaction.
        /// </summary>
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
                    throw new ArgumentException($"The currency code {value} is invalid.");
                }
                _currency = value;
            }
        }

        /// <summary>
        /// The discount code applied to the transaction. If multiple discount
        /// codes were used, please separate them with a comma.
        /// </summary>
        [JsonProperty("discount_code")]
        public string DiscountCode { get; }

        /// <summary>
        /// The ID of the affiliate where the order is coming from.
        /// </summary>
        [JsonProperty("affiliate_id")]
        public string AffiliateId { get; }

        /// <summary>
        /// The ID of the sub-affiliate where the order is coming from.
        /// </summary>
        [JsonProperty("subaffiliate_id")]
        public string SubaffiliateId { get; }

        /// <summary>
        /// The URI of the referring site for this order.
        /// </summary>
        [JsonProperty("referrer_uri")]
        public Uri ReferrerUri { get; }

        /// <summary>
        /// Whether order was marked as a gift by the purchaser.
        /// </summary>
        [JsonProperty("is_gift")]
        public bool? IsGift { get; }

        /// <summary>
        /// Whether the purchaser included a gift message.
        /// </summary>
        [JsonProperty("has_gift_message")]
        public bool? HasGiftMessage { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"Amount: {Amount}, Currency: {Currency}, DiscountCode: {DiscountCode}, AffiliateId: {AffiliateId}, SubaffiliateId: {SubaffiliateId}, ReferrerUri: {ReferrerUri}, IsGift: {IsGift}, HasGiftMessage: {HasGiftMessage}";
        }
    }
}