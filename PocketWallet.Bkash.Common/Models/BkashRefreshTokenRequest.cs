using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents Bkash refresh token request object.
    /// </summary>
    internal class BkashRefreshTokenRequest
    {
        /// <summary>
        /// Application Key value shared by Bkash during on-boarding.
        /// </summary>
        [JsonPropertyName("app_key")]
        internal string AppKey { get; set; } = string.Empty;

        /// <summary>
        /// Application Secret value shared by Bkash during on-boarding.
        /// </summary>
        [JsonPropertyName("app_secret")]
        internal string AppSecret { get; set; } = string.Empty;

        /// <summary>
        /// Refresh token value found in the Grant Token API against the original id_token.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        internal string RefreshToken { get; set; } = string.Empty;
    }
}
