using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents common base Bkash object.
    /// </summary>
    internal class BaseBkashResponse
    {
        /// <summary>
        /// Unique code assigned to the API call status.
        /// </summary>
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; } = string.Empty;

        /// <summary>
        /// Message associated with the status, explaining the status.
        /// </summary>
        [JsonPropertyName("statusMessage")]
        public string StatusMessage { get; set; } = string.Empty;

        /// <summary>
        /// Unique code assigned to the occurred error.
        /// </summary>
        [JsonPropertyName("errorCode")]
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// Message associated with the error, explaining the error reason.
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
