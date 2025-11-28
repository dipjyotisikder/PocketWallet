using Newtonsoft.Json;

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
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; } = string.Empty;

        /// <summary>
        /// Message associated with the status, explaining the status.
        /// </summary>
        [JsonProperty("statusMessage")]
        public string StatusMessage { get; set; } = string.Empty;

        /// <summary>
        /// Unique code assigned to the occurred error.
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; } = string.Empty;

        /// <summary>
        /// Message associated with the error, explaining the error reason.
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
