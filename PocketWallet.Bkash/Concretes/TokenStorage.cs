namespace PocketWallet.Bkash.Concretes
{
    internal class TokenStorage : ITokenStorage
    {
        private readonly IDateTimeProvider _timeProvider;

        public TokenStorage(IDateTimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public string AccessToken { get; private set; } = string.Empty;

        public string RefreshToken { get; private set; } = string.Empty;

        public DateTime Expiry { get; private set; }

        public bool IsEmpty() => string.IsNullOrWhiteSpace(AccessToken);

        public bool IsExpired()
        {
            if (IsEmpty())
            {
                return false;
            }

            return Expiry < _timeProvider.UtcNow;
        }

        public void Set(string accessToken, string refreshToken, DateTime expiry)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Expiry = expiry;
        }
    }
}
