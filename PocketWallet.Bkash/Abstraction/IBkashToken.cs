namespace PocketWallet.Bkash.Abstraction;
internal interface IBkashToken
{
    internal Task<Dictionary<string, string>> GetSecurityTokenHeaders();
}
