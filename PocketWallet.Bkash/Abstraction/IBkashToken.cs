namespace PocketWallet.Bkash.Abstraction;
internal interface IBkashToken
{
    internal Task<string> CreateToken();
}
