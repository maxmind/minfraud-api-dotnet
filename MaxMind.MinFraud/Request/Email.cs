using System;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class Email
    {
        private string _domain;

        public Email(
            MailAddress address = null,
            string domain = null
            )
        {
            Address = address;
            Domain = domain;
        }

        [JsonProperty("address")]
        internal string AddressMD5
        {
            get
            {
                if (Address == null)
                {
                    return null;
                }
                using (var md5Generator = MD5.Create())
                {
                    var bytes = Encoding.UTF8.GetBytes(Address.Address);
                    var md5 = md5Generator.ComputeHash(bytes);
                    return BitConverter.ToString(md5)
                        .Replace("-", string.Empty)
                        .ToLower();
                }
            }
        }

        [JsonIgnore]
        public MailAddress Address { get; }

        [JsonProperty("domain")]
        public string Domain
        {
            get { return _domain ?? Address?.Host; }
            private set
            {
                if (Uri.CheckHostName(value) == UriHostNameType.Unknown)
                {
                    throw new InvalidInputException($"The email domain {value} is not valid.");
                }
                _domain = value;
            }
        }

        public override string ToString()
        {
            return $"Address: {Address}, Domain: {Domain}";
        }
    }
}