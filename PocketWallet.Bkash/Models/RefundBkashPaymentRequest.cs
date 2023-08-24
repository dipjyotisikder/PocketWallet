namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents request object used to refund payment.
/// </summary>
internal class RefundBkashPaymentRequest
{
    /// <summary>
    /// Payment ID received during create call
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentId { get; set; } = string.Empty;

    /// <summary>
    /// Amount to refund, Maximum two decimals after amount. Ex. 25.20.
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// trxID received during execute call.
    /// </summary>
    [JsonProperty("trxID")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// To have a tagging of product information from merchant website.
    /// </summary>
    /// <remarks>Limit: String (Max Length - 255)</remarks>
    [JsonProperty("sku")]
    public string SKU { get; set; } = string.Empty;

    /// <summary>
    /// The reason to refund the payment. Ex. faulty product, product not received, etc.
    /// </summary>
    /// <remarks>Limit: String (Max Length - 255)</remarks>
    [JsonProperty("reason")]
    public string Reason { get; set; } = string.Empty;
}