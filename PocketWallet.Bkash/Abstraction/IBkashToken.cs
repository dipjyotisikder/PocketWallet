namespace PocketWallet.Bkash.Abstraction;
internal interface IBkashToken
{
    Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders();
}
