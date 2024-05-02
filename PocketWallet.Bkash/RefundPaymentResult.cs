namespace PocketWallet.Bkash;

/// <summary>
/// Represents response object used to refund payment.
/// </summary>
public class RefundPaymentResult
{
    /// <summary>
    /// When the action of refund is done.
    /// </summary>
    public string CompletedTime { get; set; } = string.Empty;

    /// <summary>
    /// Last status of the transaction.
    /// </summary>
    public string TransactionStatus { get; set; } = string.Empty;

    /// <summary>
    /// The main or originator transaction ID of that payment.
    /// </summary>
    public string OriginalTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// The Transaction Id of that refund action itself.
    /// </summary>
    public string RefundTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// How much money refunded.
    /// </summary>
    public float Amount { get; set; }

    /// <summary>
    /// The refund amount currency.
    /// </summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// If there are any charge to refund that amount, it will reflect here.
    /// </summary>
    public string Charge { get; set; } = string.Empty;
}