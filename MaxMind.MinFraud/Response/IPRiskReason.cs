using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Reason for the IP risk.
    /// </summary>
    /// <remarks>
    /// This class provides both a machine-readable code and a human-readable
    /// explanation of the reason for the IP risk score.
    /// </remarks>
    public sealed class IPRiskReason
    {
        /// <summary>
        /// This property is a machine-readable code identifying the reason.
        ///</summary>
        /// <remarks>
        /// Although more codes may be added in the future, the current codes are:
        ///
        /// <list type="table">
        /// <item>
        /// <term>ANONYMOUS_IP</term>
        /// <description>The IP address belongs to an anonymous network. See the
        /// object at <c>.IPAddress.Traits</c> for more details.</description>
        /// </item>
        /// <item>
        /// <term>BILLING_POSTAL_VELOCITY</term>
        /// <description>Many different billing postal codes have been seen on
        /// this IP address.</description>
        /// </item>
        /// <item>
        /// <term>EMAIL_VELOCITY</term>
        /// <description>Many different email addresses have been seen on this
        /// IP address.</description>
        /// </item>
        /// <item>
        /// <term>HIGH_RISK_DEVICE</term>
        /// <description>A high risk device was seen on this IP address.</description>
        /// </item>
        /// <item>
        /// <term>HIGH_RISK_EMAIL</term>
        /// <description>A high risk email address was seen on this IP address in
        /// your past transactions.</description>
        /// </item>
        /// <item>
        /// <term>ISSUER_ID_NUMBER_VELOCITY</term>
        /// <description>Many different issuer ID numbers have been seen on this
        /// IP address.</description>
        /// </item>
        /// <item>
        /// <term>MINFRAUD_NETWORK_ACTIVITY</term>
        /// <description>Suspicious activity has been seen on this IP address
        /// across minFraud customers.</description>
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