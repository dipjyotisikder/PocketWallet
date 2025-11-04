using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketWallet.Bkash.Abstraction
{
    /// <summary>
    /// Represents Bkash authorization handling tool.
    /// </summary>
    internal interface IBkashAuthorizationHandler
    {
        /// <summary>
        /// Gets Bkash authorization headers.
        /// </summary>
        /// <returns>A dictionary of authorization headers.</returns>
        Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders();
    }
}
