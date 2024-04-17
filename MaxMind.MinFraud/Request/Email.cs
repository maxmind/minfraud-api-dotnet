using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The email information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Email
    {
        private static readonly Regex DuplicateDotComRe = new Regex(@"(?:\.com){2,}$", RegexOptions.Compiled);
        private static readonly Regex GmailLeadingDigitRe = new Regex(@"^\d+(?:gmail?\.com)$", RegexOptions.Compiled);
        private static readonly IdnMapping _idn = new();
        private static readonly IReadOnlyDictionary<string, string> _typoDomains = new Dictionary<string, string>
        {
            // gmail.com
            {"gmai.com", "gmail.com"},
            {"gamil.com", "gmail.com"},
            {"gmali.com", "gmail.com"},
            {"gmial.com", "gmail.com"},
            {"gmil.com", "gmail.com"},
            {"gmaill.com", "gmail.com"},
            {"gmailm.com", "gmail.com"},
            {"gmailo.com", "gmail.com"},
            {"gmailyhoo.com", "gmail.com"},
            {"yahoogmail.com", "gmail.com"},
            // outlook.com
            {"putlook.com", "outlook.com"},
        };

        private static readonly IReadOnlyDictionary<string, string> _typoTlds = new Dictionary<string, string>
        {
            {"comm", "com"},
            {"commm", "com"},
            {"commmm", "com"},
            {"comn", "com"},

            {"cbm", "com"},
            {"ccm", "com"},
            {"cdm", "com"},
            {"cem", "com"},
            {"cfm", "com"},
            {"cgm", "com"},
            {"chm", "com"},
            {"cim", "com"},
            {"cjm", "com"},
            {"ckm", "com"},
            {"clm", "com"},
            {"cmm", "com"},
            {"cnm", "com"},
            {"cpm", "com"},
            {"cqm", "com"},
            {"crm", "com"},
            {"csm", "com"},
            {"ctm", "com"},
            {"cum", "com"},
            {"cvm", "com"},
            {"cwm", "com"},
            {"cxm", "com"},
            {"cym", "com"},
            {"czm", "com"},

            {"col", "com"},
            {"con", "com"},

            {"dom", "com"},
            {"don", "com"},
            {"som", "com"},
            {"son", "com"},
            {"vom", "com"},
            {"von", "com"},
            {"xom", "com"},
            {"xon", "com"},

            {"clam", "com"},
            {"colm", "com"},
            {"comcom", "com"},
        };

        private static readonly IReadOnlyDictionary<string, string> _equivalentDomains = new Dictionary<string, string>
        {
            {"googlemail.com", "gmail.com"},
            {"pm.me", "protonmail.com"},
            {"proton.me", "protonmail.com"},
            {"yandex.by", "yandex.ru"},
            {"yandex.com", "yandex.ru"},
            {"yandex.kz", "yandex.ru"},
            {"yandex.ua", "yandex.ru"},
            {"ya.ru", "yandex.ru"},
        };

        private static readonly HashSet<string> _fastmailDomains = new HashSet<string>
        {
            "123mail.org",
            "150mail.com",
            "150ml.com",
            "16mail.com",
            "2-mail.com",
            "4email.net",
            "50mail.com",
            "airpost.net",
            "allmail.net",
            "bestmail.us",
            "cluemail.com",
            "elitemail.org",
            "emailcorner.net",
            "emailengine.net",
            "emailengine.org",
            "emailgroups.net",
            "emailplus.org",
            "emailuser.net",
            "eml.cc",
            "f-m.fm",
            "fast-email.com",
            "fast-mail.org",
            "fastem.com",
            "fastemail.us",
            "fastemailer.com",
            "fastest.cc",
            "fastimap.com",
            "fastmail.cn",
            "fastmail.co.uk",
            "fastmail.com",
            "fastmail.com.au",
            "fastmail.de",
            "fastmail.es",
            "fastmail.fm",
            "fastmail.fr",
            "fastmail.im",
            "fastmail.in",
            "fastmail.jp",
            "fastmail.mx",
            "fastmail.net",
            "fastmail.nl",
            "fastmail.org",
            "fastmail.se",
            "fastmail.to",
            "fastmail.tw",
            "fastmail.uk",
            "fastmail.us",
            "fastmailbox.net",
            "fastmessaging.com",
            "fea.st",
            "fmail.co.uk",
            "fmailbox.com",
            "fmgirl.com",
            "fmguy.com",
            "ftml.net",
            "h-mail.us",
            "hailmail.net",
            "imap-mail.com",
            "imap.cc",
            "imapmail.org",
            "inoutbox.com",
            "internet-e-mail.com",
            "internet-mail.org",
            "internetemails.net",
            "internetmailing.net",
            "jetemail.net",
            "justemail.net",
            "letterboxes.org",
            "mail-central.com",
            "mail-page.com",
            "mailandftp.com",
            "mailas.com",
            "mailbolt.com",
            "mailc.net",
            "mailcan.com",
            "mailforce.net",
            "mailftp.com",
            "mailhaven.com",
            "mailingaddress.org",
            "mailite.com",
            "mailmight.com",
            "mailnew.com",
            "mailsent.net",
            "mailservice.ms",
            "mailup.net",
            "mailworks.org",
            "ml1.net",
            "mm.st",
            "myfastmail.com",
            "mymacmail.com",
            "nospammail.net",
            "ownmail.net",
            "petml.com",
            "postinbox.com",
            "postpro.net",
            "proinbox.com",
            "promessage.com",
            "realemail.net",
            "reallyfast.biz",
            "reallyfast.info",
            "rushpost.com",
            "sent.as",
            "sent.at",
            "sent.com",
            "speedpost.net",
            "speedymail.org",
            "ssl-mail.com",
            "swift-mail.com",
            "the-fastest.net",
            "the-quickest.com",
            "theinternetemail.com",
            "veryfast.biz",
            "veryspeedy.net",
            "warpmail.net",
            "xsmail.com",
            "yepmail.net",
            "your-mail.com"
        };

        private static readonly HashSet<string> _yahooDomains = new HashSet<string>
        {
            "y7mail.com",
            "yahoo.at",
            "yahoo.be",
            "yahoo.bg",
            "yahoo.ca",
            "yahoo.cl",
            "yahoo.co.id",
            "yahoo.co.il",
            "yahoo.co.in",
            "yahoo.co.kr",
            "yahoo.co.nz",
            "yahoo.co.th",
            "yahoo.co.uk",
            "yahoo.co.za",
            "yahoo.com",
            "yahoo.com.ar",
            "yahoo.com.au",
            "yahoo.com.br",
            "yahoo.com.co",
            "yahoo.com.hk",
            "yahoo.com.hr",
            "yahoo.com.mx",
            "yahoo.com.my",
            "yahoo.com.pe",
            "yahoo.com.ph",
            "yahoo.com.sg",
            "yahoo.com.tr",
            "yahoo.com.tw",
            "yahoo.com.ua",
            "yahoo.com.ve",
            "yahoo.com.vn",
            "yahoo.cz",
            "yahoo.de",
            "yahoo.dk",
            "yahoo.ee",
            "yahoo.es",
            "yahoo.fi",
            "yahoo.fr",
            "yahoo.gr",
            "yahoo.hu",
            "yahoo.ie",
            "yahoo.in",
            "yahoo.it",
            "yahoo.lt",
            "yahoo.lv",
            "yahoo.nl",
            "yahoo.no",
            "yahoo.pl",
            "yahoo.pt",
            "yahoo.ro",
            "yahoo.se",
            "yahoo.sk",
            "ymail.com",
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

            localPart = localPart.Normalize(NormalizationForm.FormC);
            domain = CleanDomain(domain);

            var divider = _yahooDomains.Contains(domain) ? '-' : '+';

            var dividerIdx = localPart.IndexOf(divider);
            if (dividerIdx > 0)
            {
                localPart = localPart.Substring(0, dividerIdx);
            }

            if (domain == "gmail.com")
            {
                localPart = localPart.Replace(".", "");
            }

            var domainParts = domain.Split('.');
            if (domainParts.Length > 2)
            {
                var possibleDomain = string.Join(".", domainParts.Skip(1));
                if (_fastmailDomains.Contains(possibleDomain))
                {
                    domain = possibleDomain;
                    if (!string.IsNullOrEmpty(localPart))
                    {
                        localPart = domainParts[0];
                    }
                }
            }

            return $"{localPart}@{domain}";
        }

        private static string CleanDomain(string domain)
        {
            domain = domain.Trim();

            if (domain.EndsWith("."))
            {
                char[] period = { '.' };
                domain = domain.TrimEnd(period);
            }

            domain = _idn.GetAscii(domain);

            domain = DuplicateDotComRe.Replace(domain, ".com");
            domain = GmailLeadingDigitRe.Replace(domain, "gmail.com");

            var idx = domain.LastIndexOf('.');
            if (idx != -1)
            {
                var tld = domain.Substring(idx + 1);
                if (_typoTlds.ContainsKey(tld))
                {
                    domain = domain.Substring(0, idx) + "." + _typoTlds[tld];
                }
            }

            if (_typoDomains.ContainsKey(domain))
            {
                domain = _typoDomains[domain];
            }

            if (_equivalentDomains.ContainsKey(domain))
            {
                domain = _equivalentDomains[domain];
            }

            return domain;
        }
    }
}
