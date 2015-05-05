using System.Text.RegularExpressions;
using MaxMind.MinFraud.Exception;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    public class CreditCard
    {
        private string _issuerIdNumber;
        private string _last4Digits;

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
                    throw new InvalidInputException($"The issuer ID number {value} is of the wrong format.");
                }
                _issuerIdNumber = value;
            }
        }

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
                    throw new InvalidInputException($"The last 4 credit card digits {value} is of the wrong format.");
                }
                _last4Digits = value;
            }
        }

        [JsonProperty("bank_name")]
        public string BankName { get; }

        [JsonProperty("bank_phone_country_code")]
        public string BankPhoneCountryCode { get; }

        [JsonProperty("bank_phone_number")]
        public string BankPhoneNumber { get; }

        [JsonProperty("avs_result")]
        public char? AvsResult { get; }

        [JsonProperty("cvv_result")]
        public char? CvvResult { get; }

        public override string ToString()
        {
            return
                $"IssuerIdNumber: {IssuerIdNumber}, Last4Digits: {Last4Digits}, BankName: {BankName}, BankPhoneCountryCode: {BankPhoneCountryCode}, BankPhoneNumber: {BankPhoneNumber}, AvsResult: {AvsResult}, CvvResult: {CvvResult}";
        }
    }
}