namespace PocketWallet.Bkash
{
    public class CreatePaymentResponse
    {
        /// <summary>
        /// bKash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
        /// </summary>
        public string PaymentID { get; init; } = string.Empty;

        /// <summary>
        /// The URL of bKash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
        /// </summary>
        public string BkashURL { get; init; } = string.Empty;

        /// <summary>
        /// The base URL of merchant's platform based on which bKash will generate separate callback URLs for success, failure and canceled transactions. bKash will send transaction verification result in these URLs based on the result.
        /// </summary>
        public string CallbackURL { get; init; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        public string Amount { get; init; } = string.Empty;

        /// <summary>
        /// Intent for the payment transaction. For checkout, the value will be "sale".
        /// </summary>
        public string Intent { get; init; } = string.Empty;

        /// <summary>
        /// Currency of the mentioned amount. Currently only "BDT" value is supported.
        /// </summary>
        public string Currency { get; init; } = string.Empty;

        /// <summary>
        /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
        /// </summary>
        public string PaymentCreateTime { get; init; } = string.Empty;

        /// <summary>
        /// Status of the initiated transaction. After Create Payment request, the value will be "Initiated".
        /// </summary>
        public string TransactionStatus { get; init; } = string.Empty;

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        public string MerchantInvoiceNumber { get; init; } = string.Empty;
    }
}
