namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents Bkash token request object.
/// </summary>
internal class BkashTokenRequest
{
    /// <summary>
    /// Application Key value shared by Bkash during on-boarding.
    /// </summary>
    [JsonProperty("app_key")]
    internal string AppKey { get; init; } = string.Empty;

    /// <summary>
    /// Application Secret value shared by Bkash during on-boarding.
    /// </summary>
    [JsonProperty("app_secret")]
    internal string AppSecret { get; init; } = string.Empty;
}
