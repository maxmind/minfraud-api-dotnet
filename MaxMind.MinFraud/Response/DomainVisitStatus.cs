using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// The enumerated domain visit statuses returned by the web service.
    /// </summary>
    public enum DomainVisitStatus
    {
        /// <summary>
        /// The domain is live and operational.
        /// </summary>
        [EnumMember(Value = "live")]
        Live,

        /// <summary>
        /// A DNS error was encountered when visiting the domain.
        /// </summary>
        [EnumMember(Value = "dns_error")]
        DnsError,

        /// <summary>
        /// A network error was encountered when visiting the domain.
        /// </summary>
        [EnumMember(Value = "network_error")]
        NetworkError,

        /// <summary>
        /// An HTTP error was encountered when visiting the domain.
        /// </summary>
        [EnumMember(Value = "http_error")]
        HttpError,

        /// <summary>
        /// The domain is parked.
        /// </summary>
        [EnumMember(Value = "parked")]
        Parked,

        /// <summary>
        /// The domain is in pre-development.
        /// </summary>
        [EnumMember(Value = "pre_development")]
        PreDevelopment
    }
}
