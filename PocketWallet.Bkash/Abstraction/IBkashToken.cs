namespace PocketWallet.Bkash.Abstraction;
internal interface IBkashToken
{
    internal Task<TokenResponse?> CreateInitialToken();
}
