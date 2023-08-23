namespace PocketWallet.Bkash.Abstraction;

/// <summary>
/// Represents bkash authorization handling tool.
/// </summary>
internal interface IBkashAuthorizationHandler
{
    /// <summary>
    /// Gets bkash authorization headers.
    /// </summary>
    /// <returns>A dictionary of authorization headers.</returns>
    Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders();
}
