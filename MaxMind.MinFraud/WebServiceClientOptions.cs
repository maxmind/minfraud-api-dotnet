using System;
using System.Collections.Generic;

namespace MaxMind.MinFraud
{
    /// <summary>
    /// Options class for WebServiceClient.
    /// </summary>
    public class WebServiceClientOptions
    {
        /// <summary>
        /// Your MaxMind account ID.
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// Your MaxMind license key.
        /// </summary>
        public string LicenseKey { get; set; } = string.Empty;

        /// <summary>
        /// List of locale codes to use in name property from most preferred to least preferred.
        /// </summary>
        public IEnumerable<string>? Locales { get; set; }

        /// <summary>
        /// The timeout to use for the request.
        /// </summary>
        public TimeSpan? Timeout { get; set; } = null;

        /// <summary>
        /// The host to use when accessing the service.
        /// By default, the client connects to the production host. However,
        /// during testing and development, you can set this option to
        /// 'sandbox.maxmind.com' to use the Sandbox environment's host. The
        /// sandbox allows you to experiment with the API without affecting your
        /// production data.
        /// </summary>
        public string Host { get; set; } = "minfraud.maxmind.com";
    }
}