namespace PocketWallet.Bkash.DependencyInjection.Options;

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
    public bool ProductionMode { get; set; }

    /// <summary>
    /// Takes the value of Bkash API merchant user name.
    /// </summary>
    /// <remarks>
    /// Merchant user name is the unique user name of merchant.
    /// </remarks>
    public string MerchantUserName { get; set; } = string.Empty;

    /// <summary>
    /// Takes the value of Bkash API merchant password.
    /// </summary>
    /// <remarks>
    /// Merchant password is the password value that is used to login by merchant.
    /// </remarks>
    public string MerchantPassword { get; set; } = string.Empty;

    /// <summary>
    /// Takes the value of Bkash API merchant key.
    /// </summary>
    /// <remarks>
    /// Merchant key is the key value that is found after being a merchant.
    /// </remarks>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// Takes the value of Bkash API merchant secret.
    /// </summary>
    /// <remarks>
    /// Merchant secret is the secret value that is found after being a merchant.
    /// </remarks>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// Returns the appropriate Base URL according to selected mode.
    /// </summary>
    internal string BaseURL => ProductionMode ? CONSTANTS.LIVE_BASEURL : CONSTANTS.SANDBOX_BASEURL;
}
