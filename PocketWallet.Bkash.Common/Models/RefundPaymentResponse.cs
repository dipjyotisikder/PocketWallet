using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents response object used to refund payment.
    /// </summary>
    internal class RefundPaymentResponse : BaseBkashResponse
    {
        /// <summary>
        /// When the action of refund is done.
        /// </summary>
        [JsonPropertyName("completedTime")]
        public string CompletedTime { get; set; } = string.Empty;

        /// <summary>
        /// Last status of the transaction.
        /// </summary>
        [JsonPropertyName("transactionStatus")]
        public string TransactionStatus { get; set; } = string.Empty;

        /// <summary>
        /// The main or originator transaction ID of that payment.
        /// </summary>
        [JsonPropertyName("originalTrxID")]
        public string OriginalTransactionId { get; set; } = string.Empty;

        /// <summary>
        /// The transaction ID of that refund action itself.
        /// </summary>
        [JsonPropertyName("refundTrxID")]
        public string RefundTransactionId { get; set; } = string.Empty;

        /// <summary>
        /// How much money refunded.
        /// </summary>
        [JsonPropertyName("amount")]
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// The refund amount currency.
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        /// <summary>
        /// If there are any charge to refund that amount, it will reflect here.
        /// </summary>
        [JsonPropertyName("charge")]
        public string Charge { get; set; } = string.Empty;
    }
}