using PocketWallet.Nagad.Abstraction;

namespace PocketWallet.Nagad.Concretes;

/// <summary>
/// Represents a global date time provider.
/// </summary>
internal class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public DateTime UtcNow => DateTime.UtcNow;
}
