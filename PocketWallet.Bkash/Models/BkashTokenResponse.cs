namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents Bkash token response.
/// </summary>
internal class BkashTokenResponse : BaseBkashResponse
{
    /// <summary>
    /// The expiry time of the token. By default the lifetime is 3600 seconds.
    /// </summary>
    [JsonPropertyName("expires_in")]
    internal string ExpiresIn { get; init; } = string.Empty;

    /// <summary>
    /// Corresponding token value to be used for future authorization.
    /// </summary>
    [JsonPropertyName("id_token")]
    internal string IdToken { get; init; } = string.Empty;

    /// <summary>
    /// This value should be used in Refresh Token API for getting a new token against the current token value. New tokens using this refresh_token can be found for at most 28 days. After that new token has to be created using Grant Token.
    /// </summary>
    [JsonPropertyName("refresh_token")]
    internal string RefreshToken { get; init; } = string.Empty;

    /// <summary>
    /// Token type for whom the token is being granted. Default value is "Bearer".
    /// </summary>
    [JsonPropertyName("token_type")]
    internal string TokenType { get; init; } = string.Empty;
}
