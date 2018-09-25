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

        // Note: when naming new payment processors, do
        // _not_ make uppercase letters in the middle of words uppercase in
        // for the member name (unless it is a two-letter acronym, in which
        // case it should follow Microsoft guidelines and be all uppercase),
        // e.g.:
        //
        // Hipay - (not HiPay) for the company "HiPay"
        // LemonWay - for the company "Lemon Way"
        // IO  - for the compnany "IO" (made up)
        // Ebs - for the company "EBS"
        //
        // A letter should be uppercase iif it is (1) the first letter of
        // the enum, (2) the enum value has an underscore before that letter
        // (e.g., lemon_way), (3) it is part of a two-letter acronym.

        [EnumMember(Value = "adyen")]
        Adyen,

        [EnumMember(Value = "altapay")]
        Altapay,

        [EnumMember(Value = "amazon_payments")]
        AmazonPayments,

        [EnumMember(Value = "american_express_payment_gateway")]
        AmericanExpressPaymentGateway,

        [EnumMember(Value = "authorizenet")]
        Authorizenet,

        [EnumMember(Value = "balanced")]
        Balanced,

        [EnumMember(Value = "beanstream")]
        Beanstream,

        [EnumMember(Value = "bluepay")]
        Bluepay,

        [EnumMember(Value = "bluesnap")]
        Bluesnap,

        [EnumMember(Value = "bpoint")]
        Bpoint,

        [EnumMember(Value = "braintree")]
        Braintree,

        [EnumMember(Value = "ccavenue")]
        Ccavenue,

        [EnumMember(Value = "ccnow")]
        Ccnow,

        [EnumMember(Value = "chase_paymentech")]
        ChasePaymentech,

        [EnumMember(Value = "checkout_com")]
        CheckoutCom,

        [EnumMember(Value = "cielo")]
        Cielo,

        [EnumMember(Value = "collector")]
        Collector,

        [EnumMember(Value = "compropago")]
        Compropago,

        [EnumMember(Value = "commdoo")]
        Commdoo,

        [EnumMember(Value = "concept_payments")]
        ConceptPayments,

        [EnumMember(Value = "conekta")]
        Conekta,

        [EnumMember(Value = "ct_payments")]
        CtPayments,

        [EnumMember(Value = "cuentadigital")]
        Cuentadigital,

        [EnumMember(Value = "curopayments")]
        Curopayments,

        [EnumMember(Value = "cybersource")]
        Cybersource,


        [EnumMember(Value = "dalenys")]
        Dalenys,

        [EnumMember(Value = "dalpay")]
        Dalpay,

        [EnumMember(Value = "datacash")]
        DataCash,

        [EnumMember(Value = "dibs")]
        Dibs,

        [EnumMember(Value = "digital_river")]
        DigitalRiver,

        [EnumMember(Value = "ebs")]
        Ebs,

        [EnumMember(Value = "ecomm365")]
        Ecomm365,

        [EnumMember(Value = "elavon")]
        Elavon,

        [EnumMember(Value = "emerchantpay")]
        Emerchantpay,

        [EnumMember(Value = "epay")]
        Epay,

        [EnumMember(Value = "eprocessing_network")]
        EprocessingNetwork,

        [EnumMember(Value = "eway")]
        Eway,

        [EnumMember(Value = "exact")]
        Exact,

        [EnumMember(Value = "first_data")]
        FirstData,

        [EnumMember(Value = "gocardless")]
        GoCardless,

        [EnumMember(Value = "global_payments")]
        GlobalPayments,

        [EnumMember(Value = "heartland")]
        Heartland,

        [EnumMember(Value = "hipay")]
        Hipay,

        [EnumMember(Value = "ingenico")]
        Ingenico,

        [EnumMember(Value = "internetsecure")]
        Internetsecure,

        [EnumMember(Value = "intuit_quickbooks_payments")]
        IntuitQuickbooksPayments,

        [EnumMember(Value = "iugu")]
        Iugu,

        [EnumMember(Value = "lemon_way")]
        LemonWay,

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

        [EnumMember(Value = "oceanpayment")]
        Oceanpayment,

        [EnumMember(Value = "oney")]
        Oney,

        [EnumMember(Value = "other")]
        Other,

        [EnumMember(Value = "openpaymx")]
        Openpaymx,

        [EnumMember(Value = "optimal_payments")]
        OptimalPayments,

        [EnumMember(Value = "orangepay")]
        Orangepay,

        [EnumMember(Value = "pacnet_services")]
        PacnetServices,

        [EnumMember(Value = "payeezy")]
        Payeezy,

        [EnumMember(Value = "payfast")]
        Payfast,

        [EnumMember(Value = "paygate")]
        Paygate,

        [EnumMember(Value = "payment_express")]
        PaymentExpress,

        [EnumMember(Value = "paymentwall")]
        Paymentwall,

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

        [EnumMember(Value = "payway")]
        Payway,

        [EnumMember(Value = "pinpayments")]
        Pinpayments,

        [EnumMember(Value = "payza")]
        Payza,

        [EnumMember(Value = "posconnect")]
        Posconnect,

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

        [EnumMember(Value = "securetrading")]
        Securetrading,

        [EnumMember(Value = "simplify_commerce")]
        SimplifyCommerce,

        [EnumMember(Value = "skrill")]
        Skrill,

        [EnumMember(Value = "smartcoin")]
        Smartcoin,

        [EnumMember(Value = "smartdebit")]
        SmartDebit,

        [EnumMember(Value = "solidtrust_pay")]
        SolidtrustPay,

        [EnumMember(Value = "sps_decidir")]
        SpsDecidir,

        [EnumMember(Value = "stripe")]
        Stripe,

        [EnumMember(Value = "synapsefi")]
        SynapseFI,

        [EnumMember(Value = "telerecargas")]
        Telerecargas,

        [EnumMember(Value = "towah")]
        Towah,

        [EnumMember(Value = "transact_pro")]
        TransactPro,

        [EnumMember(Value = "usa_epay")]
        UsaEpay,

        [EnumMember(Value = "vantiv")]
        Vantiv,

        [EnumMember(Value = "verepay")]
        Verepay,

        [EnumMember(Value = "vericheck")]
        Vericheck,

        [EnumMember(Value = "vindicia")]
        Vindicia,

        [EnumMember(Value = "virtual_card_services")]
        VirtualCardServices,

        [EnumMember(Value = "vme")]
        Vme,

        [EnumMember(Value = "vpos")]
        Vpos,

        [EnumMember(Value = "wirecard")]
        Wirecard,

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
