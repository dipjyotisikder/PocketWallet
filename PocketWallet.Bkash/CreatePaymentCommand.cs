namespace PocketWallet.Bkash
{
    /// <summary>
    /// Represents payment creation object.
    /// </summary>
    public sealed class CreatePaymentCommand
    {
        /// <summary>
        /// A predefined phone/account number or any related reference, which can be passed along with the payment request. If provided, it will be pre-populated in Bkash's wallet number entry page.
        /// </summary>
        /// <remarks>This field is optional.</remarks>
        public string PayerReference { get; set; } = " ";

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        /// <remarks>This field is required.</remarks>
        public string MerchantInvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// The base URL of merchant's platform based on which Bkash will generate separate callback URLs for success, failure and canceled transactions. Bkash will send transaction verification result in these URLs based on the result.
        /// </summary>
        /// <remarks>This field is required.</remarks>
        public string CallbackURL { get; init; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        /// <remarks>This field is required.</remarks>
        public float Amount { get; init; }

        /// <summary>
        /// Intent for the payment transaction. For checkout, the value will be "sale".
        /// </summary>
        public string Intent { get; init; } = CONSTANTS.SALE;

        /// <summary>
        /// Currency of the mentioned amount. Currently only "BDT" value is supported.
        /// </summary>
        public string Currency { get; init; } = CONSTANTS.BDT;
    }
}
