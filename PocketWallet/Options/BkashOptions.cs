namespace PocketWallet.Options;

/// <summary>
/// Represents configuration values required to integrate with Bkash services.
/// </summary>
public class BkashOptions
{
    /// <summary>
    /// Gets or sets the merchant username provided by Bkash.
    /// </summary>
    public string MerchantUserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the merchant password provided by Bkash.
    /// </summary>
    public string MerchantPassword { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application key assigned to the merchant.
    /// </summary>
    public string AppKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the application secret assigned to the merchant.
    /// </summary>
    public string AppSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether production mode is enabled.
    /// </summary>
    public bool ProductionMode { get; set; }
}
