using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MaxMind.MinFraud.Request
{
    /// <summary>
    /// Enumerated payment processors supported by the web service.
    /// </summary>
    public enum PaymentProcessor
    {
#pragma warning disable CS1591

        [EnumMember(Value = "adyen")]
        Adyen,

        [EnumMember(Value = "altapay")]
        Altapay,

        [EnumMember(Value = "amazon_payments")]
        AmazonPayments,

        [EnumMember(Value = "authorizenet")]
        Authorizenet,

        [EnumMember(Value = "balanced")]
        Balanced,

        [EnumMember(Value = "beanstream")]
        Beanstream,

        [EnumMember(Value = "bluepay")]
        Bluepay,

        [EnumMember(Value = "braintree")]
        Braintree,

        [EnumMember(Value = "ccnow")]
        Ccnow,

        [EnumMember(Value = "chase_paymentech")]
        ChasePaymentech,

        [EnumMember(Value = "cielo")]
        Cielo,

        [EnumMember(Value = "collector")]
        Collector,

        [EnumMember(Value = "compropago")]
        Compropago,

        [EnumMember(Value = "conekta")]
        Conekta,

        [EnumMember(Value = "cuentadigital")]
        Cuentadigital,

        [EnumMember(Value = "dalpay")]
        Dalpay,

        [EnumMember(Value = "dibs")]
        Dibs,

        [EnumMember(Value = "digital_river")]
        DigitalRiver,

        [EnumMember(Value = "elavon")]
        Elavon,

        [EnumMember(Value = "epay")]
        Epay,

        [EnumMember(Value = "eprocessing_network")]
        EprocessingNetwork,

        [EnumMember(Value = "eway")]
        Eway,

        [EnumMember(Value = "first_data")]
        FirstData,

        [EnumMember(Value = "global_payments")]
        GlobalPayments,

        [EnumMember(Value = "ingenico")]
        Ingenico,

        [EnumMember(Value = "internetsecure")]
        Internetsecure,

        [EnumMember(Value = "intuit_quickbooks_payments")]
        IntuitQuickbooksPayments,

        [EnumMember(Value = "iugu")]
        Iugu,

        [EnumMember(Value = "mastercard_payment_gateway")]
        MastercardPaymentGateway,

        [EnumMember(Value = "mercadopago")]
        Mercadopago,

        [EnumMember(Value = "merchant_esolutions")]
        MerchantEsolutions,

        [EnumMember(Value = "mirjeh")]
        Mirjeh,

        [EnumMember(Value = "mollie")]
        Mollie,

        [EnumMember(Value = "moneris_solutions")]
        MonerisSolutions,

        [EnumMember(Value = "nmi")]
        Nmi,

        [EnumMember(Value = "other")]
        Other,

        [EnumMember(Value = "openpaymx")]
        Openpaymx,

        [EnumMember(Value = "optimal_payments")]
        OptimalPayments,

        [EnumMember(Value = "payfast")]
        Payfast,

        [EnumMember(Value = "paygate")]
        Paygate,

        [EnumMember(Value = "payone")]
        Payone,

        [EnumMember(Value = "paypal")]
        Paypal,

        [EnumMember(Value = "payplus")]
        Payplus,

        [EnumMember(Value = "paystation")]
        Paystation,

        [EnumMember(Value = "paytrace")]
        Paytrace,

        [EnumMember(Value = "paytrail")]
        Paytrail,

        [EnumMember(Value = "payture")]
        Payture,

        [EnumMember(Value = "payu")]
        Payu,

        [EnumMember(Value = "payulatam")]
        Payulatam,

        [EnumMember(Value = "pinpayments")]
        Pinpayments,

        [EnumMember(Value = "princeton_payment_solutions")]
        PrincetonPaymentSolutions,

        [EnumMember(Value = "psigate")]
        Psigate,

        [EnumMember(Value = "qiwi")]
        Qiwi,

        [EnumMember(Value = "quickpay")]
        Quickpay,

        [EnumMember(Value = "raberil")]
        Raberil,

        [EnumMember(Value = "rede")]
        Rede,

        [EnumMember(Value = "redpagos")]
        Redpagos,

        [EnumMember(Value = "rewardspay")]
        Rewardspay,

        [EnumMember(Value = "sagepay")]
        Sagepay,

        [EnumMember(Value = "simplify_commerce")]
        SimplifyCommerce,

        [EnumMember(Value = "skrill")]
        Skrill,

        [EnumMember(Value = "smartcoin")]
        Smartcoin,

        [EnumMember(Value = "sps_decidir")]
        SpsDecidir,

        [EnumMember(Value = "stripe")]
        Stripe,

        [EnumMember(Value = "telerecargas")]
        Telerecargas,

        [EnumMember(Value = "towah")]
        Towah,

        [EnumMember(Value = "usa_epay")]
        UsaEpay,

        [EnumMember(Value = "verepay")]
        Verepay,

        [EnumMember(Value = "vindicia")]
        Vindicia,

        [EnumMember(Value = "virtual_card_services")]
        VirtualCardServices,

        [EnumMember(Value = "vme")]
        Vme,

        [EnumMember(Value = "worldpay")]
        Worldpay

#pragma warning restore
    }

    /// <summary>
    /// The payment information for the transaction being sent to the
    /// web service.
    /// </summary>
    public sealed class Payment
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="processor">The payment processor used for the
        /// transaction.</param>
        /// <param name="wasAuthorized">The authorization outcome from the
        /// payment processor. If the transaction has not yet been approved
        /// or denied, do not include this field.</param>
        /// <param name="declineCode">The decline code as provided by your
        /// payment processor. If the transaction was not declined, do not
        /// include this field.</param>
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

        /// <summary>
        /// The payment processor used for the transaction.
        /// </summary>
        [JsonProperty("processor")]
        public PaymentProcessor? Processor { get; }

        /// <summary>
        /// The authorization outcome from the payment processor. If the
        /// transaction has not yet been approved or denied, do not include
        /// this field.
        /// </summary>
        [JsonProperty("was_authorized")]
        public bool? WasAuthorized { get; }

        /// <summary>
        /// The decline code as provided by your payment processor. If the
        /// transaction was not declined, do not include this field.
        /// </summary>
        [JsonProperty("decline_code")]
        public string DeclineCode { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Processor: {Processor}, WasAuthorized: {WasAuthorized}, DeclineCode: {DeclineCode}";
        }
    }
}