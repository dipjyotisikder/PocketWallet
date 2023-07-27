namespace PocketWallet.Bkash.Models
{
    public class BaseBkashResponse
    {
        /// <summary>
        /// Unique code assigned to the API call status.
        /// </summary>
        [JsonProperty("statusCode")]
        public string? StatusCode { get; init; }

        /// <summary>
        /// Message associated with the status, explaining the status.
        /// </summary>
        [JsonProperty("statusMessage")]
        public string? StatusMessage { get; init; }

        /// <summary>
        /// Unique code assigned to the occurred error.
        /// </summary>
        [JsonProperty("errorCode")]
        public string? ErrorCode { get; init; }

        /// <summary>
        /// Message associated with the error, explaining the error reason.
        /// </summary>
        [JsonProperty("errorMessage")]
        public string? ErrorMessage { get; init; }
    }
}
