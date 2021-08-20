using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The credit card information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class CreditCard
    {
        private static readonly Regex IssuerIdNumberRe = new Regex("^[0-9]{6}$", RegexOptions.Compiled);
        private static readonly Regex Last4Re = new Regex("^[0-9]{4}$", RegexOptions.Compiled);

        private static readonly Regex TokenRe = new Regex("^(?![0-9]{1,19}$)[\\x21-\\x7E]{1,255}$",
            RegexOptions.Compiled);
        private string? _issuerIdNumber;
        private string? _last4Digits;
        private string? _token;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="issuerIdNumber">The issuer ID number for the credit card.
        /// This is the first 6 digits of the credit card number. It identifies
        /// the issuing bank.</param>
        /// <param name="last4Digits">The last four digits of the credit card
        /// number.</param>
        /// <param name="bankName">The name of the issuing bank as provided by
        /// the end user</param>
        /// <param name="bankPhoneCountryCode">The phone country code for the
        ///  issuing bank as provided by the end user.</param>
        /// <param name="bankPhoneNumber">The phone number, without the
        /// country code, for the issuing bank as provided by the end
        /// user.</param>
        /// <param name="avsResult">The address verification system (AVS)
        /// check result, as returned to you by the credit card processor.
        /// The minFraud service supports the standard AVS codes.</param>
        /// <param name="cvvResult">The card verification value (CVV) code
        ///  as provided by the payment processor.</param>
        /// <param name="token">A token uniquely identifying the card. This
        /// should not be the actual credit card number.</param>
        /// <param name="was3dSecureSuccessful">Whether or not the 3DS check was
        /// successful, as provided by the end user.</param>
        public CreditCard(
            string? issuerIdNumber = null,
            string? last4Digits = null,
            string? bankName = null,
            string? bankPhoneCountryCode = null,
            string? bankPhoneNumber = null,
            char? avsResult = null,
            char? cvvResult = null,
            string? token = null,
            bool? was3dSecureSuccessful = null
        )
        {
            IssuerIdNumber = issuerIdNumber;
            Last4Digits = last4Digits;
            BankName = bankName;
            BankPhoneCountryCode = bankPhoneCountryCode;
            BankPhoneNumber = bankPhoneNumber;
            AvsResult = avsResult;
            CvvResult = cvvResult;
            Token = token;
            Was3dSecureSuccessful = was3dSecureSuccessful;
        }

        /// <summary>
        /// The issuer ID number for the credit card. This is the first 6
        /// digits of the credit card number. It identifies the issuing bank.
        /// </summary>
        [JsonPropertyName("issuer_id_number")]
        public string? IssuerIdNumber
        {
            get => _issuerIdNumber;
            init
            {
                if (value != null && !IssuerIdNumberRe.IsMatch(value))
                {
                    throw new ArgumentException($"The issuer ID number {value} is of the wrong format.");
                }
                _issuerIdNumber = value;
            }
        }

        /// <summary>
        /// The last four digits of the credit card number.
        /// </summary>
        [JsonPropertyName("last_4_digits")]
        public string? Last4Digits
        {
            get => _last4Digits;
            init
            {
                if (value != null && !Last4Re.IsMatch(value))
                {
                    throw new ArgumentException($"The last 4 credit card digits {value} is of the wrong format.");
                }
                _last4Digits = value;
            }
        }

        /// <summary>
        /// The name of the issuing bank as provided by the end user.
        /// </summary>
        [JsonPropertyName("bank_name")]
        public string? BankName { get; init; }

        /// <summary>
        /// The phone country code for the issuing bank as provided by
        /// the end user.
        /// </summary>
        [JsonPropertyName("bank_phone_country_code")]
        public string? BankPhoneCountryCode { get; init; }

        /// <summary>
        /// The phone number, without the country code, for the
        /// issuing bank as provided by the end user.
        /// </summary>
        [JsonPropertyName("bank_phone_number")]
        public string? BankPhoneNumber { get; init; }

        /// <summary>
        /// The address verification system (AVS) check result, as
        /// returned to you by the credit card processor. The minFraud
        /// service supports the standard AVS codes.
        /// </summary>
        [JsonPropertyName("avs_result")]
        public char? AvsResult { get; init; }

        /// <summary>
        /// The card verification value (CVV) code as provided by the
        /// payment processor.
        /// </summary>
        [JsonPropertyName("cvv_result")]
        public char? CvvResult { get; init; }

        /// <summary>
        /// A token uniquely identifying the card. This should not be
        /// the actual credit card number.
        /// </summary>
        [JsonPropertyName("token")]
        public string? Token
        {
            get => _token;
            init
            {
                if (value != null && !TokenRe.IsMatch(value))
                {
                    throw new ArgumentException($"The credit card token {value} was invalid. "
                                                + "Tokens must be non-space ASCII printable characters. If the "
                                                + "token consists of all digits, it must be more than 19 digits.");
                }
                _token = value;
            }
        }

        /// <summary>
        /// Whether or not the 3DS check was successful, as provided by the end
        /// user.
        /// </summary>
        [JsonPropertyName("was_3d_secure_successful")]
        public bool? Was3dSecureSuccessful { get; init; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return
                $"IssuerIdNumber: {IssuerIdNumber}, Last4Digits: {Last4Digits}, BankName: {BankName}, BankPhoneCountryCode: {BankPhoneCountryCode}, BankPhoneNumber: {BankPhoneNumber}, AvsResult: {AvsResult}, CvvResult: {CvvResult}, Token: {Token}, Was3dSecureSuccessful: {Was3dSecureSuccessful}";
        }
    }
}