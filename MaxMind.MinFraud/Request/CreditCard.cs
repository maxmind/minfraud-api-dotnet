using System;
using System.Text.RegularExpressions;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// The credit card information for the transaction being sent to the
    /// web service.
    /// </summary>
    public class CreditCard
    {
        private string _issuerIdNumber;
        private string _last4Digits;

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
        public CreditCard(
            string issuerIdNumber = null,
            string last4Digits = null,
            string bankName = null,
            string bankPhoneCountryCode = null,
            string bankPhoneNumber = null,
            char? avsResult = null,
            char? cvvResult = null
            )
        {
            IssuerIdNumber = issuerIdNumber;
            Last4Digits = last4Digits;
            BankName = bankName;
            BankPhoneCountryCode = bankPhoneCountryCode;
            BankPhoneNumber = bankPhoneNumber;
            AvsResult = avsResult;
            CvvResult = cvvResult;
        }

        /// <summary>
        /// The issuer ID number for the credit card. This is the first 6 
        /// digits of the credit card number. It identifies the issuing bank.
        /// </summary>
        [JsonProperty("issuer_id_number")]
        public string IssuerIdNumber
        {
            get { return _issuerIdNumber; }
            private set
            {
                if (value == null)
                {
                    return;
                }
                var re = new Regex("^[0-9]{6}$");
                if (!re.IsMatch(value))
                {
                    throw new ArgumentException($"The issuer ID number {value} is of the wrong format.");
                }
                _issuerIdNumber = value;
            }
        }

        /// <summary>
        /// The last four digits of the credit card number.
        /// </summary>
        [JsonProperty("last_4_digits")]
        public string Last4Digits
        {
            get { return _last4Digits; }
            private set
            {
                if (value == null)
                {
                    return;
                }
                var re = new Regex("^[0-9]{4}$");
                if (!re.IsMatch(value))
                {
                    throw new ArgumentException($"The last 4 credit card digits {value} is of the wrong format.");
                }
                _last4Digits = value;
            }
        }

        /// <summary>
        /// The name of the issuing bank as provided by the end user.
        /// </summary>
        [JsonProperty("bank_name")]
        public string BankName { get; }

        /// <summary>
        /// The phone country code for the issuing bank as provided by
        /// the end user.
        /// </summary>
        [JsonProperty("bank_phone_country_code")]
        public string BankPhoneCountryCode { get; }

        /// <summary>
        /// The phone number, without the country code, for the
        /// issuing bank as provided by the end user.
        /// </summary>
        [JsonProperty("bank_phone_number")]
        public string BankPhoneNumber { get; }

        /// <summary>
        /// The address verification system (AVS) check result, as
        /// returned to you by the credit card processor. The minFraud 
        /// service supports the standard AVS codes.
        /// </summary>
        [JsonProperty("avs_result")]
        public char? AvsResult { get; }

        /// <summary>
        /// The card verification value (CVV) code as provided by the
        /// payment processor.
        /// </summary>
        [JsonProperty("cvv_result")]
        public char? CvvResult { get; }

        public override string ToString()
        {
            return
                $"IssuerIdNumber: {IssuerIdNumber}, Last4Digits: {Last4Digits}, BankName: {BankName}, BankPhoneCountryCode: {BankPhoneCountryCode}, BankPhoneNumber: {BankPhoneNumber}, AvsResult: {AvsResult}, CvvResult: {CvvResult}";
        }
    }
}