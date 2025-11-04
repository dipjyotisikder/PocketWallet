namespace PocketWallet.Bkash
{
    /// <summary>
    /// Result object for payment creation.
    /// </summary>
    public class CreatePaymentResult
    {
        /// <summary>
        /// Bkash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
        /// </summary>
        public string PaymentId { get; set; } = string.Empty;

        /// <summary>
        /// The base URL of merchant's platform based on which Bkash will generate separate callback URLs for success, failure and canceled transactions. Bkash will send transaction verification result in these URLs based on the result.
        /// </summary>
        public string CallbackURL { get; set; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// The URL of Bkash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
        /// </summary>
        public string BkashURL { get; set; } = string.Empty;

        /// <summary>
        /// Intent for the payment transaction. For checkout, the value will be "sale".
        /// </summary>
        public string Intent { get; set; } = string.Empty;

        /// <summary>
        /// Currency of the mentioned amount. Currently only "BDT" value is supported.
        /// </summary>
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
        /// </summary>
        public string PaymentCreateTime { get; set; } = string.Empty;

        /// <summary>
        /// Status of the initiated transaction. After Create Payment request, the value will be "Initiated".
        /// </summary>
        public string TransactionStatus { get; set; } = string.Empty;

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        public string MerchantInvoiceNumber { get; set; } = string.Empty;
    }
}
