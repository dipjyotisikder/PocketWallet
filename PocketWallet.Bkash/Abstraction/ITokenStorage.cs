namespace PocketWallet.Bkash.Abstraction
{
    internal interface ITokenStorage
    {
        string AccessToken { get; }

        string RefreshToken { get; }

        bool IsExpired();

        bool IsEmpty();

        void Set(string accessToken, string refreshToken, DateTime expiry);

    }
}
