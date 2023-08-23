namespace PocketWallet.Bkash;

/// <summary>
/// Payment execution response after fully completed payment operation.
/// </summary>
public class ExecutePaymentResult
{
    /// <summary>
    /// bKash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
    /// </summary>
    public string PaymentId { get; init; } = string.Empty;

    /// <summary>
    /// The payer reference value that was provided during the create payment API call.
    /// </summary>
    public string PayerReference { get; init; } = string.Empty;

    /// <summary>
    /// MSISDN of the customer or phone number who performed the payment.
    /// </summary>
    public string CustomerMSISDN { get; init; } = string.Empty;

    /// <summary>
    /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
    /// </summary>
    public string TransactionId { get; init; } = string.Empty;

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    public string Amount { get; init; } = string.Empty;

    /// <summary>
    /// Final status of the transaction. For a successful payment, this status should be "Completed".
    /// </summary>
    public string TransactionStatus { get; init; } = string.Empty;

    /// <summary>
    /// The time when the payment was executed. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    public string PaymentExecuteTime { get; init; } = string.Empty;

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
    public string MerchantInvoiceNumber { get; init; } = string.Empty;
}