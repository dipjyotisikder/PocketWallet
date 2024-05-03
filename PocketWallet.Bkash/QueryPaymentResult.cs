namespace PocketWallet.Bkash;

/// <summary>
/// Represents payment information.
/// </summary>
public class QueryPaymentResult
{
    /// <summary>
    /// bKash generated payment ID for this payment creation request.
    /// </summary>
    public string PaymentId { get; init; } = string.Empty;

    /// <summary>
    /// bKash generated payment ID for this payment creation request.
    /// </summary>
    public string Mode { get; init; } = string.Empty;

    /// <summary>
    /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    public string PaymentCreateTime { get; init; } = string.Empty;

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    public string Amount { get; init; } = string.Empty;

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    public string Currency { get; init; } = string.Empty;

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    public string Intent { get; init; } = string.Empty;

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    public string MerchantInvoice { get; init; } = string.Empty;

    /// <summary>
    /// Final status of the transaction. For a successful payment, this status should be "Completed". Otherwise, the status will be "Initiated".
    /// </summary>
    public string TransactionStatus { get; init; } = string.Empty;

    /// <summary>
    /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
    /// </summary>
    public string VerificationStatus { get; init; } = string.Empty;

    /// <summary>
    /// Any related reference value which was passed along with the payment request. If the wallet number is passed , then it will be pre-populated in bKash's wallet number entry page.
    /// </summary>
    public string PayerReference { get; init; } = string.Empty;

    /// <summary>
    /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
    /// </summary>
    public string TransactionId { get; init; } = string.Empty;

    /// <summary>
    /// The time when the payment was executed. Format is "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    public string PaymentExecuteTime { get; init; } = string.Empty;

    /// <summary>
    /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
    /// </summary>
    public string UserVerificationStatus { get; init; } = string.Empty;
}