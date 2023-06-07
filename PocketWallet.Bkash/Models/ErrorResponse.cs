namespace PocketWallet.Bkash.Models;

public class ErrorResponse
{
    /// <summary>
    /// Unique code assigned to the occurred error.
    /// </summary>
    [JsonProperty("errorCode")]
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Message associated with the error, explaining the error reason.
    /// </summary>
    [JsonProperty("errorMessage")]
    public string? ErrorMessage { get; set; }
}
