using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// This object contains the disposition set by custom rules.
    /// </summary>
    public sealed class Disposition
    {
        /// <summary>
        /// The action to take on the transaction as defined by your custom
        /// rules. The current set of values are "accept", "manual_review",
        /// and "reject". If you do not have custom rules set up, <c>null</c>
        /// will be returned.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("action")]
        public string? Action { get; internal set; }

        /// <summary>
        /// The reason for the action. The current possible values are
        /// "custom_rule", "block_list", and "default". If you do not have
        /// custom rules set up, <c>null</c> will be returned.
        /// </summary>
        [JsonInclude]
        [JsonPropertyName("reason")]
        public string? Reason { get; internal set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Action: {Action}, Reason: {Reason}";
        }
    }
}