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
        /// "reject" and "test". If you do not have custom rules set up,
        /// <c>null</c> will be returned.
        /// </summary>
        [JsonPropertyName("action")]
        public string? Action { get; init; }

        /// <summary>
        /// The reason for the action. The current possible values are
        /// "custom_rule" and "default". If you do not have custom rules set
        /// up, <c>null</c> will be returned.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; init; }

        /// <summary>
        /// The label of the custom rule that was triggered. If you do not have
        /// custom rules set up, the triggered custom rule does not have a
        /// label, or no custom rule was triggered, <c>null</c> will be
        /// returned.
        /// </summary>
        [JsonPropertyName("rule_label")]
        public string? RuleLabel { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return $"Action: {Action}, Reason: {Reason}, Rule Label: {RuleLabel}";
        }
    }
}
