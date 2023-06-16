namespace PocketWallet.Bkash.DependencyInjection;

/// <summary>
/// Configuration option to enable Bkash API.
/// </summary>
public record BkashConfigurationOptions
{
    /// <summary>
    /// Takes the value of Bkash API mode.
    /// </summary>
    /// <remarks>
    /// If the mode is set to true, app will run on production mode and real payment will happen.
    /// </remarks>
    public bool ProductionMode { get; init; }

    /// <summary>
    /// Takes the value of Bkash API merchant key.
    /// </summary>
    /// <remarks>
    /// Merchant key describes the key value that is found after being a merchant.
    /// </remarks>
    public string? MerchantKey { get; init; }

    /// <summary>
    /// Takes the value of Bkash API merchant secret.
    /// </summary>
    /// <remarks>
    /// Merchant secret describes the secret value that is found after being a merchant.
    /// </remarks>
    public string? MerchantSecret { get; init; }

    /// <summary>
    /// Takes the value of Bkash API merchant user name.
    /// </summary>
    /// <remarks>
    /// Merchant user name describes the unique user name of merchant.
    /// </remarks>
    public string? MerchantUserName { get; init; }

    /// <summary>
    /// Takes the value of Bkash API merchant password.
    /// </summary>
    /// <remarks>
    /// Merchant password describes the password value that is used to login by merchant.
    /// </remarks>
    public string? MerchantPassword { get; init; }

    internal string BaseURL => ProductionMode ? CONSTANTS.LIVE_BASEURL : CONSTANTS.SANDBOX_BASEURL;
}
