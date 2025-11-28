using Newtonsoft.Json;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents request object used to query payment information.
    /// </summary>
    internal class QueryPaymentRequest
    {
        /// <summary>
        /// Bkash generated paymentID provided in the response of Create Payment API.
        /// </summary>
        [JsonProperty("paymentID")]
        public string PaymentId { get; set; } = string.Empty;
    }
}