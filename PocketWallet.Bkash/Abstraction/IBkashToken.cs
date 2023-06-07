namespace PocketWallet.Bkash.Abstraction;
internal interface IBkashToken
{
    internal Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders();
}
