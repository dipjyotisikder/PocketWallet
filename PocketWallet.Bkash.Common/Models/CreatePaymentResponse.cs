using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents payment creation response object.
    /// </summary>
    internal class CreatePaymentResponse : BaseBkashResponse
    {
        /// <summary>
        /// Bkash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
        /// </summary>
        [JsonPropertyName("paymentID")]
        public string PaymentId { get; set; } = string.Empty;

        /// <summary>
        /// The URL of Bkash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
        /// </summary>
        [JsonPropertyName("bkashURL")]
        public string BkashURL { get; set; } = string.Empty;

        /// <summary>
        /// The base URL of merchant's platform based on which Bkash will generate separate callback URLs for success, failure and canceled transactions. Bkash will send transaction verification result in these URLs based on the result.
        /// </summary>
        [JsonPropertyName("callbackURL")]
        public string CallbackURL { get; set; } = string.Empty;

        /// <summary>
        /// The success callback URL where Bkash will inform merchant about the transaction result in case of a successful transaction.
        /// </summary>
        [JsonPropertyName("successCallbackURL")]
        public string SuccessCallbackURL { get; set; } = string.Empty;

        /// <summary>
        /// The failure callback URL where Bkash will inform merchant about the transaction result in case of a failed transaction.
        /// </summary>
        [JsonPropertyName("failureCallbackURL")]
        public string FailureCallbackURL { get; set; } = string.Empty;

        /// <summary>
        /// The canceled callback URL where Bkash will inform merchant about the transaction result in case of a canceled transaction.
        /// </summary>
        [JsonPropertyName("cancelledCallbackURL")]
        public string CanceledCallbackURL { get; set; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        [JsonPropertyName("amount")]
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// Intent for the payment transaction. For checkout, the value will be "sale".
        /// </summary>
        [JsonPropertyName("intent")]
        public string Intent { get; set; } = string.Empty;

        /// <summary>
        /// Currency of the mentioned amount. Currently only "BDT" value is supported.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
        /// </summary>
        [JsonPropertyName("paymentCreateTime")]
        public string PaymentCreateTime { get; set; } = string.Empty;

        /// <summary>
        /// Status of the setiated transaction. After Create Payment request, the value will be "Initiated".
        /// </summary>
        [JsonPropertyName("transactionStatus")]
        public string TransactionStatus { get; set; } = string.Empty;

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        [JsonPropertyName("merchantInvoiceNumber")]
        public string MerchantInvoiceNumber { get; set; } = string.Empty;
    }
}