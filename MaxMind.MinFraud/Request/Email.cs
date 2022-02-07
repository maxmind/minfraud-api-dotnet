using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The email information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Email
    {
        private static readonly IdnMapping _idn = new();
        private static readonly IReadOnlyDictionary<string, string> _typoDomains = new Dictionary<string, string>
        {
            // gmail.com
            {"35gmai.com", "gmail.com"},
            {"636gmail.com", "gmail.com"},
            {"gamil.com", "gmail.com"},
            {"gmail.comu", "gmail.com"},
            {"gmial.com", "gmail.com"},
            {"gmil.com", "gmail.com"},
            {"yahoogmail.com", "gmail.com"},
            // outlook.com
            {"putlook.com", "outlook.com"},
        };

        private string? _address;
        private string? _domain;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="address">The user's email address. This will be
        /// converted into an MD5 before being sent to the web service.
        /// </param>
        /// <param name="domain">The domain of the email address used in the
        /// transaction. If <c>address</c> is passed to the constructor
        /// and <c>domain</c> is not, the domain will be automatically
        /// set from the address.</param>
        /// <param name="hashAddress">By default, the <c>address</c> will
        /// be sent in plain text. If <c>hashAddress</c> is set to true,
        /// the address will instead be sent as an MD5 hash.</param>
        public Email(
            string? address = null,
            string? domain = null,
            bool hashAddress = false
        )
        {
            Address = address;
            Domain = domain;
            HashAddress = hashAddress;
        }

        /// <summary>
        /// The MD5 generated from the email address.
        /// </summary>
        [JsonIgnore]
        public string? AddressMD5
        {
            get
            {
                if (Address == null)
                    return null;

                using var md5Generator = MD5.Create();
                var bytes = Encoding.UTF8.GetBytes(CleanAddress(Address));
                var md5 = md5Generator.ComputeHash(bytes);
                return BitConverter.ToString(md5)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
        }

        /// <summary>
        /// The email address used in the transaction.
        /// </summary>
        [JsonIgnore]
        public string? Address
        {
            get => _address;
            init
            {
                if (value == null)
                {
                    return;
                }
                var parts = value.Split('@');
                if (parts.Length < 2)
                {
                    throw new ArgumentException($"The email address {value} is invalid");
                }

                if (_domain == null)
                {
                    _domain = parts.Last();
                }
                _address = value;
            }
        }

        /// <summary>
        ///     The address value that will be sent in the request.
        /// </summary>
        [JsonPropertyName("address")]
        public string? RequestAddress => HashAddress ? AddressMD5 : Address;

        /// <summary>
        /// The domain of the email address.
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain
        {
            get => _domain;
            init
            {
                if (value == null)
                {
                    return;
                }
                if (Uri.CheckHostName(value) == UriHostNameType.Unknown)
                {
                    throw new ArgumentException($"The email domain {value} is not valid.");
                }
                _domain = value;
            }
        }

        /// <summary>
        /// By default, the <c>address</c> will  be sent in plain text. If
        /// this is set to true, the address will instead be sent as an MD5
        /// hash.
        /// </summary>
        [JsonIgnore]
        public bool HashAddress { get; init; } = false;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Address: {Address}, Domain: {Domain}";
        }

        private static string CleanAddress(string address)
        {
            address = address.Trim().ToLower();

            var domainIndex = address.LastIndexOf('@');
            if (domainIndex == -1 || domainIndex + 1 == address.Length)
            {
                return address;
            }

            var localPart = address.Substring(0, domainIndex);
            var domain = address.Substring(domainIndex + 1);

            domain = CleanDomain(domain);

            var divider = domain == "yahoo.com" ? '-' : '+';

            var dividerIdx = localPart.IndexOf(divider);
            if (dividerIdx > 0)
            {
                localPart = localPart.Substring(0, dividerIdx);
            }

            return $"{localPart}@{domain}";
        }

        private static string CleanDomain(string domain)
        {
            domain = domain.Trim();

            if (domain.EndsWith("."))
            {
                domain = domain.Substring(0, domain.Length - 1);
            }

            domain = _idn.GetAscii(domain);

            if (_typoDomains.ContainsKey(domain))
            {
                domain = _typoDomains[domain];
            }

            return domain;
        }
    }
}