namespace PocketWallet.Bkash.Concretes
{
    /// <summary>
    /// Represents token storage mechanism tool.
    /// </summary>
    internal class TokenStorage : ITokenStorage
    {
        private readonly IDateTimeProvider _timeProvider;

        private DateTime _expiry;
        private string _accessToken = string.Empty;
        private string _refreshToken = string.Empty;

        public TokenStorage(IDateTimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        /// <inheritdoc/>
        public string AccessToken => _accessToken;

        /// <inheritdoc/>
        public string RefreshToken => _refreshToken;

        /// <inheritdoc/>
        public bool IsTokenAvailable => !string.IsNullOrWhiteSpace(_accessToken);

        /// <inheritdoc/>
        public bool IsTokenAlive => IsTokenAvailable && _expiry > _timeProvider.UtcNow;

        /// <inheritdoc/>
        public void Set(string accessToken, string refreshToken, DateTime expiry)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
            _expiry = expiry;
        }
    }
}
