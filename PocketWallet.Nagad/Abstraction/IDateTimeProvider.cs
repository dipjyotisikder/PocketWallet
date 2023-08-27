namespace PocketWallet.Nagad.Abstraction;

/// <summary>
/// Represents a global date time provider.
/// </summary>
internal interface IDateTimeProvider
{
    /// <summary>
    /// Provides current UTC date time.
    /// </summary>
    DateTime UtcNow { get; }
}
