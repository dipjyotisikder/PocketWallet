namespace PocketWallet.Bkash.Models;
internal class TokenResponse
{
    [JsonProperty("expires_in")]
    internal string? ExpiresIn { get; set; }

    [JsonProperty("id_token")]
    internal string? IdToken { get; set; }

    [JsonProperty("refresh_token")]
    internal string? RefreshToken { get; set; }

    [JsonProperty("token_type")]
    internal string? TokenType { get; set; }

    /// <summary>
    /// If request is failed or not.
    /// </summary>
    /// <remarks>
    /// if (response.status === 'fail') throw new BkashException('Invalid API Credentials Provided');
    /// </remarks>
    [JsonProperty("status")]
    internal string? Status { get; set; }

    [JsonProperty("msg")]
    internal string? Msg { get; set; }
}
