namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents actual Bkash response for execution of payment.
/// </summary>
internal class ExecutePaymentResponse : BaseBkashResponse
{
    /// <summary>
    /// Bkash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentId { get; init; } = string.Empty;

    /// <summary>
    /// The payer reference value that was provided during the create payment API call.
    /// </summary>
    [JsonProperty("payerReference")]
    public string PayerReference { get; init; } = string.Empty;

    /// <summary>
    /// MSISDN of the customer who performed the payment.
    /// </summary>
    [JsonProperty("customerMsisdn")]
    public string CustomerMSISDN { get; init; } = string.Empty;

    /// <summary>
    /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
    /// </summary>
    [JsonProperty("trxID")]
    public string TransactionId { get; init; } = string.Empty;

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; init; } = string.Empty;

    /// <summary>
    /// Final status of the transaction. For a successful payment, this status should be "Completed".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string TransactionStatus { get; init; } = string.Empty;

    /// <summary>
    /// The time when the payment was executed. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentExecuteTime")]
    public string PaymentExecuteTime { get; init; } = string.Empty;

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; init; } = string.Empty;

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string Intent { get; init; } = string.Empty;

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string MerchantInvoiceNumber { get; init; } = string.Empty;
}