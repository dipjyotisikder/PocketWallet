using Newtonsoft.Json;

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
        [JsonProperty("paymentID")]
        public string PaymentId { get; set; } = string.Empty;

        /// <summary>
        /// TransactionId received during execute call.
        /// </summary>
        [JsonProperty("trxID")]
        public string TransactionId { get; set; } = string.Empty;
    }
}
