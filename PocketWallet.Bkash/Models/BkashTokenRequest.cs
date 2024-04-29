namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents bkash token request object.
/// </summary>
internal class BkashTokenRequest
{
    /// <summary>
    /// Application Key value shared by bkash during on-boarding.
    /// </summary>
    [JsonProperty("app_key")]
    internal string AppKey { get; init; } = string.Empty;

    /// <summary>
    /// Application Secret value shared by bkash during on-boarding.
    /// </summary>
    [JsonProperty("app_secret")]
    internal string AppSecret { get; init; } = string.Empty;
}
