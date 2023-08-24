namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents request object used to refund payment.
/// </summary>
public class RefundPaymentCommand
{
    /// <summary>
    /// Payment Id received during create call.
    /// </summary>
    public string PaymentId { get; set; } = string.Empty;

    /// <summary>
    /// Amount to refund, Maximum two decimals after amount. Ex. 25.20.
    /// </summary>
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// trxID received during execute call.
    /// </summary>
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// To have a tagging of product information from merchant website.
    /// </summary>
    /// <remarks>Use string having maximum length of 255 characters.</remarks>
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// The reason to refund the payment. Ex. faulty product, product not received, etc.
    /// </summary>
    /// <remarks>Use string having maximum length of 255 characters.</remarks>
    public string Reason { get; set; } = string.Empty;
}