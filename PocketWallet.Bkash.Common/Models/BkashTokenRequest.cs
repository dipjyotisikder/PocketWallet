using System.Text.Json.Serialization;

namespace PocketWallet.Bkash.Common.Models
{
    /// <summary>
    /// Represents Bkash token request object.
    /// </summary>
    internal class BkashTokenRequest
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
    }
}
