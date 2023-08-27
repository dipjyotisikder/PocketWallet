namespace PocketWallet.Nagad.Models;

/// <summary>
/// Represents common base Nagad object.
/// </summary>
internal class BaseNagadResponse
{
    /// <summary>
    /// Unique code assigned to the API call status.
    /// </summary>
    [JsonProperty("statusCode")]
    public string StatusCode { get; init; } = string.Empty;

    /// <summary>
    /// Message associated with the status, explaining the status.
    /// </summary>
    [JsonProperty("statusMessage")]
    public string StatusMessage { get; init; } = string.Empty;

    /// <summary>
    /// Unique code assigned to the occurred error.
    /// </summary>
    [JsonProperty("errorCode")]
    public string ErrorCode { get; init; } = string.Empty;

    /// <summary>
    /// Message associated with the error, explaining the error reason.
    /// </summary>
    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; init; } = string.Empty;
}
