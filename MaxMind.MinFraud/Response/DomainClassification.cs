using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Response
{
    /// <summary>
    /// The enumerated domain classifications returned by the web service.
    /// </summary>
    public enum DomainClassification
    {
        /// <summary>
        /// The domain is associated with a business.
        /// </summary>
        [EnumMember(Value = "business")]
        Business,

        /// <summary>
        /// The domain is associated with an educational institution.
        /// </summary>
        [EnumMember(Value = "education")]
        Education,

        /// <summary>
        /// The domain is associated with a government entity.
        /// </summary>
        [EnumMember(Value = "government")]
        Government,

        /// <summary>
        /// The domain is an ISP-provided email service.
        /// </summary>
        [EnumMember(Value = "isp_email")]
        IspEmail
    }
}
