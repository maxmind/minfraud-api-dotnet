using System.Net;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class Device
    {
        public Device(
            IPAddress ipAddress,
            string userAgent = null,
            string acceptLanguage = null
            )
        {
            IPAddress = ipAddress;
            UserAgent = userAgent;
            AcceptLanguage = acceptLanguage;
        }

        [JsonProperty("ip_address")]
        public IPAddress IPAddress { get; }

        [JsonProperty("user_agent")]
        public string UserAgent { get; }

        [JsonProperty("accept_language")]
        public string AcceptLanguage { get; }

        public override string ToString()
        {
            return $"IPAddress: {IPAddress}, UserAgent: {UserAgent}, AcceptLanguage: {AcceptLanguage}";
        }
    }
}