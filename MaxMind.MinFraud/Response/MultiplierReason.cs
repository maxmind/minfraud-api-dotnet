using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// The risk score reason for the multiplier.
    /// </summary>
    /// <remarks>
    /// This class provides both a machine-readable code and a human-readable
    /// explanation of the reason for the risk score, see
    /// <a href="https://dev.maxmind.com/minfraud/api-documentation/responses/#schema--response--risk-score-reason--multiplier-reason">the documentation</a>.
    /// </remarks>
    public sealed class MultiplierReason
    {
        /// <summary>
        /// This property is a machine-readable code identifying the reason.
        ///</summary>
        /// <remarks>
        /// Although more codes may be added in the future, the current codes are:
        ///
        /// <list type="table">
        /// <item>
        /// <term>BROWSER_LANGUAGE</term>
        /// <description>Riskiness of the browser user-agent and language associated
        /// with the request.</description>
        /// </item>
        /// <item>
        /// <term>BUSINESS_ACTIVITY</term>
        /// <description>Riskiness of business activity associated with the request.</description>
        /// </item>
        /// <item>
        /// <term>COUNTRY</term>
        /// <description>Riskiness of the country associated with the request.</description>
        /// </item>
        /// <item>
        /// <term>CUSTOMER_ID</term>
        /// <description>Riskiness of a customer's activity.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_DOMAIN</term>
        /// <description>Riskiness of email domain.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_DOMAIN_NEW</term>
        /// <description>Riskiness of newly-sighted email domain.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_ADDRESS_NEW</term>
        /// <description>Riskiness of newly-sighted email address.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_LOCAL_PART</term>
        /// <description>Riskiness of the local part of the email address.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_VELOCITY</term>
        /// <description>Velocity on email - many requests on same email
        /// over short period of time.</description>
        /// </item>
        /// <item>
        /// <term>ISSUER_ID_NUMBER_COUNTRY_MISMATCH</term>
        /// <description>Riskiness of the country mismatch between IP,
        /// billing, shipping and IIN country.</description>
        /// </item>
        /// <item>
        /// <term>ISSUER_ID_NUMBER_ON_SHOP_ID</term>
        /// <description>Risk of Issuer ID Number for the shop ID.</description>
        /// </item>
        /// <item>
        /// <term>ISSUER_ID_NUMBER_LAST_DIGITS_ACTIVITY</term>
        /// <description>Riskiness of many recent requests and previous high-risk requests
        /// on the IIN and last digits of the credit card.</description>
        /// </item>
        /// <item>
        /// <term>ISSUER_ID_NUMBER_SHOP_ID_VELOCITY</term>
        /// <description>Risk of recent Issuer ID Number activity for the shop ID.</description>
        /// </item>
        /// <item>
        /// <term>INTRACOUNTRY_DISTANCE</term>
        /// <description>Risk of distance between IP, billing, and shipping location.</description>
        /// </item>
        /// <item>
        /// <term>ANONYMOUS_IP</term>
        /// <description>Risk due to IP being an Anonymous IP.</description>
        /// </item>
        /// <item>
        /// <term>IP_BILLING_POSTAL_VELOCITY</term>
        /// <description>Velocity of distinct billing postal code on IP address.</description>
        /// </item>
        /// <item>
        /// <term>IP_EMAIL_VELOCITY</term>
        /// <description>Velocity of distinct email address on IP address.</description>
        /// </item>
        /// <item>
        /// <term>IP_HIGH_RISK_DEVICE</term>
        /// <description>High-risk device sighted on IP address.</description>
        /// </item>
        /// <item>
        /// <term>IP_ISSUER_ID_NUMBER_VELOCITY</term>
        /// <description>Velocity of distinct IIN on IP address.</description>
        /// </item>
        /// <item>
        /// <term>IP_ACTIVITY</term>
        /// <description>Riskiness of IP based on minFraud network activity.</description>
        /// </item>
        /// <item>
        /// <term>LANGUAGE</term>
        /// <description>Riskiness of browser language.</description>
        /// </item>
        /// <item>
        /// <term>MAX_RECENT_EMAIL</term>
        /// <description>Riskiness of email address based on
        /// past minFraud risk scores on email.</description>
        /// </item>
        /// <item>
        /// <term>MAX_RECENT_PHONE</term>
        /// <description>Riskiness of phone number based on
        /// past minFraud risk scores on phone.</description>
        /// </item>
        /// <item>
        /// <term>MAX_RECENT_SHIP</term>
        /// <description>Riskiness of email address based on
        /// past minFraud risk scores on ship address.</description>
        /// </item>
        /// <item>
        /// <term>MULTIPLE_CUSTOMER_ID_ON_EMAIL</term>
        /// <description>Riskiness of email address having many customer IDs.</description>
        /// </item>
        /// <item>
        /// <term>ORDER_AMOUNT</term>
        /// <description>Riskiness of the order amount.</description>
        /// </item>
        /// <item>
        /// <term>ORG_DISTANCE_RISK</term>
        /// <description>Risk of ISP and distance between billing address
        /// and IP location.</description>
        /// </item>
        /// <item>
        /// <term>PHONE</term>
        /// <description>Riskiness of the phone number or related numbers.</description>
        /// </item>
        /// <item>
        /// <term>CART</term>
        /// <description>Riskiness of shopping cart contents.</description>
        /// </item>
        /// <item>
        /// <term>TIME_OF_DAY</term>
        /// <description>Risk due to local time of day.</description>
        /// </item>
        /// <item>
        /// <term>TRANSACTION_REPORT_EMAIL</term>
        /// <description>Risk due to transaction reports on the email address.</description>
        /// </item>
        /// <item>
        /// <term>TRANSACTION_REPORT_IP</term>
        /// <description>Risk due to transaction reports on the IP address.</description>
        /// </item>
        /// <item>
        /// <term>TRANSACTION_REPORT_PHONE</term>
        /// <description>Risk due to transaction reports on the phone number.</description>
        /// </item>
        /// <item>
        /// <term>TRANSACTION_REPORT_SHIP</term>
        /// <description>Risk due to transaction reports on the shipping address.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_ACTIVITY</term>
        /// <description>Riskiness of the email address based on minFraud network activity.</description>
        /// </item>
        /// <item>
        /// <term>PHONE_ACTIVITY</term>
        /// <description>Riskiness of the phone number based on minFraud network activity.</description>
        /// </item>
        /// <item>
        /// <term>SHIP_ACTIVITY</term>
        /// <description>Riskiness of ship address based on minFraud network activity.</description>
        /// </item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("code")]
        public string? Code { get; init; }

        /// <summary>
        /// This property provides a human-readable explanation of the reason.
        /// The description may change at any time and should not be matched
        /// against.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Code: {Code}, Reason: {Reason}";
        }
    }
}
