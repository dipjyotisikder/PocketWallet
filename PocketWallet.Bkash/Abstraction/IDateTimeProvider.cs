namespace PocketWallet.Bkash.Abstraction
{
    internal interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
