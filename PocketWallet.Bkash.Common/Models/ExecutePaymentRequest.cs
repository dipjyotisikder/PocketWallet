using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents payment execution request object.
    /// </summary>
    internal class ExecutePaymentRequest
    {
        /// <summary>
        /// PaymentID returned in the response of Create Payment API.
        /// </summary>
        [JsonPropertyName("paymentID")]
        public string PaymentId { get; set; } = string.Empty;
    }
}
