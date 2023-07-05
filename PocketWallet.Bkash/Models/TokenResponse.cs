namespace PocketWallet.Bkash.Models;
internal class TokenResponse
{
    /// <summary>
    /// The expiry time of the token. By default the lifetime is 3600 seconds.
    /// </summary>
    [JsonProperty("expires_in")]
    internal string? ExpiresIn { get; init; }

    /// <summary>
    /// Corresponding token value to be used for future authorization.
    /// </summary>
    [JsonProperty("id_token")]
    internal string? IdToken { get; init; }

    /// <summary>
    /// This value should be used in Refresh Token API for getting a new token against the current token value. New tokens using this refresh_token can be found for at most 28 days. After that new token has to be created using Grant Token.
    /// </summary>
    [JsonProperty("refresh_token")]
    internal string? RefreshToken { get; init; }

    /// <summary>
    /// Token type for whom the token is being granted. Default value is "Bearer".
    /// </summary>
    [JsonProperty("token_type")]
    internal string? TokenType { get; init; }

    /// <summary>
    /// The response code indicating response status.
    /// </summary>
    /// <remarks>
    /// 'fail'
    /// </remarks>
    [JsonProperty("statusCode")]
    internal string? StatusCode { get; init; }

    /// <summary>
    /// Response message corresponding to the statusCode.
    /// </summary>
    [JsonProperty("statusMessage")]
    internal string? StatusMessage { get; init; }
}
