namespace PocketWallet.Bkash
{
    /// <summary>
    /// Represents payment creation object.
    /// </summary>
    public sealed class CreatePaymentCommand
    {
        /// <summary>
        /// A predefined phone/account number or any related reference, which can be passed along with the payment request. If provided, it will be pre-populated in bkash's wallet number entry page.
        /// </summary>
        public string PayerReference { get; set; } = " ";

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        public string MerchantInvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// The base URL of merchant's platform based on which bkash will generate separate callback URLs for success, failure and canceled transactions. bkash will send transaction verification result in these URLs based on the result.
        /// </summary>
        public string CallbackURL { get; init; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        public float Amount { get; init; }
    }
}
