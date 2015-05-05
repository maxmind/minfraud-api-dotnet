using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    public class Score
    {
        [JsonProperty("credits_remaining")]
        public ulong? CreditsRemaining { get; internal set; }

        [JsonProperty("id")]
        public Guid? Id { get; internal set; }

        [JsonProperty("risk_score")]
        public double? RiskScore { get; internal set; }

        [JsonProperty("warnings")]
        public List<Warning> Warnings { get; internal set; } = new List<Warning>();

        public override string ToString()
        {
            var warnings = string.Join("; ", Warnings.Select(x => x.Message));
            return $"Warnings: [{warnings}], CreditsRemaining: {CreditsRemaining}, Id: {Id}, RiskScore: {RiskScore}";
        }
    }
}