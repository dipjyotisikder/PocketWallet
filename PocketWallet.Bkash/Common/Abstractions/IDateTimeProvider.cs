using System;

namespace PocketWallet.Bkash.Common.Abstractions
{
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
}
