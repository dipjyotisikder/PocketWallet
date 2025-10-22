using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents request object used to know refund status.
    /// </summary>
    internal class RefundStatusRequest
    {
        /// <summary>
        /// Payment Id received during create call.
        /// </summary>
        [JsonPropertyName("paymentID")]
        public string PaymentId { get; set; } = string.Empty;

        /// <summary>
        /// TransactionId received during execute call.
        /// </summary>
        [JsonPropertyName("trxID")]
        public string TransactionId { get; set; } = string.Empty;
    }
}
