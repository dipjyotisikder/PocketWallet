using PocketWallet.Bkash.Common.Abstractions;
using System;

namespace PocketWallet.Bkash.Common.Providers
{
    /// <summary>
    /// Represents a global date time provider.
    /// </summary>
    internal class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
