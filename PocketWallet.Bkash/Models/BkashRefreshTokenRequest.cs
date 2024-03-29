﻿namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents bkash refresh token request object.
/// </summary>
internal class BkashRefreshTokenRequest
{
    /// <summary>
    /// Application Key value shared by bKash during on-boarding.
    /// </summary>
    [JsonProperty("app_key")]
    internal string AppKey { get; init; } = string.Empty;

    /// <summary>
    /// Application Secret value shared by bKash during on-boarding.
    /// </summary>
    [JsonProperty("app_secret")]
    internal string AppSecret { get; init; } = string.Empty;

    /// <summary>
    /// Refresh token value found in the Grant Token API against the original id_token.
    /// </summary>
    [JsonProperty("refresh_token")]
    internal string RefreshToken { get; init; } = string.Empty;
}
