namespace PocketWallet.Bkash.Abstraction;

/// <summary>
/// Represents token storage mechanism tool.
/// </summary>
internal interface ITokenStorage
{
    /// <summary>
    /// Gets current access token.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets current refresh token.
    /// </summary>
    string RefreshToken { get; }

    /// <summary>
    /// Gets current access token expiration status.
    /// </summary>
    bool IsTokenAlive { get; }

    /// <summary>
    /// Gets current access token's availability.
    /// </summary>
    bool IsTokenAvailable { get; }

    /// <summary>
    /// Sets complete access token object.
    /// </summary>
    /// <param name="accessToken">Access token.</param>
    /// <param name="refreshToken">Refresh token.</param>
    /// <param name="expiry">Access token expiration time.</param>
    void Set(string accessToken, string refreshToken, DateTime expiry);
}
