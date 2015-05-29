using System.Net;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The device information for the transaction being sent to the
    /// web service.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ipAddress">The IP address associated with the device
        /// used by the customer in the transaction.</param>
        /// <param name="userAgent">The HTTP “User-Agent” header of the
        /// browser used in the transaction.</param>
        /// <param name="acceptLanguage">The HTTP “Accept-Language” header of
        /// the device used in the transaction.</param>
        public Device(
            IPAddress ipAddress,
            string userAgent = null,
            string acceptLanguage = null
            )
        {
            this.IPAddress = ipAddress;
            UserAgent = userAgent;
            AcceptLanguage = acceptLanguage;
        }

        /// <summary>
        /// The IP address associated with the device used by the customer
        /// in the transaction.
        /// </summary>
        [JsonProperty("ip_address")]
        public IPAddress IPAddress { get; }

        /// <summary>
        /// The HTTP “User-Agent” header of the browser used in the
        /// transaction.
        /// </summary>
        [JsonProperty("user_agent")]
        public string UserAgent { get; }

        /// <summary>
        /// The HTTP “Accept-Language” header of the device used in the
        /// transaction.
        /// </summary>
        [JsonProperty("accept_language")]
        public string AcceptLanguage { get; }

        public override string ToString()
        {
            return $"IPAddress: {IPAddress}, UserAgent: {UserAgent}, AcceptLanguage: {AcceptLanguage}";
        }
    }
}
