using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace MaxMind.MinFraud.Request
{
    // XXX - fix enum serialization
    public enum PaymentProcessor
    {
        [EnumMember(Value = "adyen")] Adyen,
        [EnumMember(Value = "altapay")] Altapay,
        [EnumMember(Value = "amazon_payments")] AmazonPayments,
        [EnumMember(Value = "authorizenet")] Authorizenet,
        [EnumMember(Value = "balanced")] Balanced,
        [EnumMember(Value = "beanstream")] Beanstream,
        [EnumMember(Value = "bluepay")] Bluepay,
        [EnumMember(Value = "braintree")] Braintree,
        [EnumMember(Value = "chase_paymentech")] ChasePaymentech,
        [EnumMember(Value = "cielo")] Cielo,
        [EnumMember(Value = "collector")] Collector,
        [EnumMember(Value = "compropago")] Compropago,
        [EnumMember(Value = "conekta")] Conekta,
        [EnumMember(Value = "cuentadigital")] Cuentadigital,
        [EnumMember(Value = "dibs")] Dibs,
        [EnumMember(Value = "digital_river")] DigitalRiver,
        [EnumMember(Value = "elavon")] Elavon,
        [EnumMember(Value = "epayeu")] Epayeu,
        [EnumMember(Value = "eprocessing_network")] EprocessingNetwork,
        [EnumMember(Value = "eway")] Eway,
        [EnumMember(Value = "first_data")] FirstData,
        [EnumMember(Value = "global_payments")] GlobalPayments,
        [EnumMember(Value = "ingenico")] Ingenico,
        [EnumMember(Value = "internetsecure")] Internetsecure,
        [EnumMember(Value = "intuit_quickbooks_payments")] IntuitQuickbooksPayments,
        [EnumMember(Value = "iugu")] Iugu,
        [EnumMember(Value = "mastercard_payment_gateway")] MastercardPaymentGateway,
        [EnumMember(Value = "mercadopago")] Mercadopago,
        [EnumMember(Value = "merchant_esolutions")] MerchantEsolutions,
        [EnumMember(Value = "mirjeh")] Mirjeh,
        [EnumMember(Value = "mollie")] Mollie,
        [EnumMember(Value = "moneris_solutions")] MonerisSolutions,
        [EnumMember(Value = "nmi")] Nmi,
        [EnumMember(Value = "other")] Other,
        [EnumMember(Value = "openpaymx")] Openpaymx,
        [EnumMember(Value = "optimal_payments")] OptimalPayments,
        [EnumMember(Value = "payfast")] Payfast,
        [EnumMember(Value = "paygate")] Paygate,
        [EnumMember(Value = "payone")] Payone,
        [EnumMember(Value = "paypal")] Paypal,
        [EnumMember(Value = "paystation")] Paystation,
        [EnumMember(Value = "paytrace")] Paytrace,
        [EnumMember(Value = "paytrail")] Paytrail,
        [EnumMember(Value = "payture")] Payture,
        [EnumMember(Value = "payu")] Payu,
        [EnumMember(Value = "payulatam")] Payulatam,
        [EnumMember(Value = "princeton_payment_solutions")] PrincetonPaymentSolutions,
        [EnumMember(Value = "psigate")] Psigate,
        [EnumMember(Value = "qiwi")] Qiwi,
        [EnumMember(Value = "raberil")] Raberil,
        [EnumMember(Value = "rede")] Rede,
        [EnumMember(Value = "redpagos")] Redpagos,
        [EnumMember(Value = "rewardspay")] Rewardspay,
        [EnumMember(Value = "sagepay")] Sagepay,
        [EnumMember(Value = "simplify_commerce")] SimplifyCommerce,
        [EnumMember(Value = "skrill")] Skrill,
        [EnumMember(Value = "smartcoin")] Smartcoin,
        [EnumMember(Value = "sps_decidir")] SpsDecidir,
        [EnumMember(Value = "stripe")] Stripe,
        [EnumMember(Value = "telerecargas")] Telerecargas,
        [EnumMember(Value = "towah")] Towah,
        [EnumMember(Value = "usa_epay")] UsaEpay,
        [EnumMember(Value = "vindicia")] Vindicia,
        [EnumMember(Value = "virtual_card_services")] VirtualCardServices,
        [EnumMember(Value = "vme")] Vme,
        [EnumMember(Value = "worldpay")] Worldpay
    }

    public class Payment
    {
        public Payment(
            PaymentProcessor? processor = null,
            bool? wasAuthorized = null,
            string declineCode = null
            )
        {
            Processor = processor;
            WasAuthorized = wasAuthorized;
            DeclineCode = declineCode;
        }

        [JsonProperty("processor")]
        public PaymentProcessor? Processor { get; }

        [JsonProperty("was_authorized")]
        public bool? WasAuthorized { get; }

        [JsonProperty("decline_code")]
        public string DeclineCode { get; }

        public override string ToString()
        {
            return $"Processor: {Processor}, WasAuthorized: {WasAuthorized}, DeclineCode: {DeclineCode}";
        }
    }
}