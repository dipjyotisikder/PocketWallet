using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents actual response received from Bkash.
    /// </summary>
    internal class QueryPaymentResponse : BaseBkashResponse
    {
        /// <summary>
        /// Bkash generated payment ID for this payment creation request.
        /// </summary>
        [JsonPropertyName("paymentID")]
        public string PaymentId { get; set; } = string.Empty;

        /// <summary>
        /// Bkash generated payment ID for this payment creation request.
        /// </summary>
        [JsonPropertyName("mode")]
        public string Mode { get; set; } = string.Empty;

        /// <summary>
        /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
        /// </summary>
        [JsonPropertyName("paymentCreateTime")]
        public string PaymentCreateTime { get; set; } = string.Empty;

        /// <summary>
        /// The time when the payment was executed. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
        /// </summary>
        [JsonPropertyName("paymentExecuteTime")]
        public string PaymentExecuteTime { get; set; } = string.Empty;

        /// <summary>
        /// Amount of the payment transaction.
        /// </summary>
        [JsonPropertyName("amount")]
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// Currency of the mentioned amount. Currently only "BDT" value is supported.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// Intent for the payment transaction. For checkout, the value will be "sale".
        /// </summary>
        [JsonPropertyName("intent")]
        public string Intent { get; set; } = string.Empty;

        /// <summary>
        /// Unique invoice number used at merchant side for this specific payment.
        /// </summary>
        [JsonPropertyName("merchantInvoice")]
        public string MerchantInvoice { get; set; } = string.Empty;

        /// <summary>
        /// Final status of the transaction. For a successful payment, this status should be "Completed". Otherwise, the status will be "Initiated".
        /// </summary>
        [JsonPropertyName("transactionStatus")]
        public string TransactionStatus { get; set; } = string.Empty;

        /// <summary>
        /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
        /// </summary>
        [JsonPropertyName("verificationStatus")]
        public string VerificationStatus { get; set; } = string.Empty;

        /// <summary>
        /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
        /// </summary>
        [JsonPropertyName("userVerificationStatus")]
        public string UserVerificationStatus { get; set; } = string.Empty;

        /// <summary>
        /// Any related reference value which was passed along with the payment request. If the wallet number is passed , then it will be pre-populated in Bkash's wallet number entry page.
        /// </summary>
        [JsonPropertyName("payerReference")]
        public string PayerReference { get; set; } = string.Empty;

        /// <summary>
        /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
        /// </summary>
        [JsonPropertyName("trxID")]
        public string TransactionId { get; set; } = string.Empty;
    }
}