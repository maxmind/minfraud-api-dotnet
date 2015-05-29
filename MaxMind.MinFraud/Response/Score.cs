using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// Model class for Score response.
    /// </summary>
    public class Score
    {
        [JsonProperty("warnings")]
        private List<Warning> _warnings = new List<Warning>();

        /// <summary>
        /// The approximate number of service credits remaining on your 
        /// account.
        /// </summary>
        [JsonProperty("credits_remaining")]
        public ulong? CreditsRemaining { get; internal set; }

        /// <summary>
        /// This is a UUID that identifies the minFraud request. Please use
        /// this ID in support requests to MaxMind so that we can easily 
        /// identify a particular request.
        /// </summary>
        [JsonProperty("id")]
        public Guid? Id { get; internal set; }

        /// <summary>
        /// This property contains the risk score, from 0.01 to 99. A
        /// higher score indicates a higher risk of fraud.For example, a score of 20
        /// indicates a 20% chance that a transaction is fraudulent.We never return a
        /// risk score of 0, since all transactions have the possibility of being
        /// fraudulent.Likewise we never return a risk score of 100.
        /// </summary>
        [JsonProperty("risk_score")]
        public double? RiskScore { get; internal set; }

        /// <summary>
        /// This list contains objects detailing issues with the request that 
        /// was sent such as invalid or unknown inputs. It is highly 
        /// recommended that you check this array for issues when integrating 
        /// the web service.
        /// </summary>
        [JsonIgnore]
        public List<Warning> Warnings => new List<Warning>(_warnings);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            var warnings = string.Join("; ", Warnings.Select(x => x.Message));
            return $"Warnings: [{warnings}], CreditsRemaining: {CreditsRemaining}, Id: {Id}, RiskScore: {RiskScore}";
        }
    }
}