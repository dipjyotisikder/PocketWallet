namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents response object used to refund payment.
/// </summary>
internal class RefundBkashPaymentResponse : BaseBkashResponse
{
    /// <summary>
    /// When the action of refund is done.
    /// </summary>
    [JsonProperty("completedTime")]
    public string CompletedTime { get; set; } = string.Empty;

    /// <summary>
    /// Last status of the transaction.
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string TransactionStatus { get; set; } = string.Empty;

    /// <summary>
    /// The main or originator transaction ID of that payment.
    /// </summary>
    [JsonProperty("originalTrxID")]
    public string OriginalTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// The transaction ID of that refund action itself.
    /// </summary>
    [JsonProperty("refundTrxID")]
    public string RefundTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// How much money refunded.
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// The refund amount currency.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// If there are any charge to refund that amount, it will reflect here.
    /// </summary>
    [JsonProperty("charge")]
    public string Charge { get; set; } = string.Empty;
}