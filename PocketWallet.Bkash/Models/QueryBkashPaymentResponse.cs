namespace PocketWallet.Bkash.Models;

internal class QueryBkashPaymentResponse : BaseBkashResponse
{
    /// <summary>
    /// bKash generated payment ID for this payment creation request.
    /// </summary>
    [JsonProperty("paymentID")]
    public string? PaymentID { get; init; }

    /// <summary>
    /// bKash generated payment ID for this payment creation request.
    /// </summary>
    [JsonProperty("mode")]
    public string? Mode { get; init; }

    /// <summary>
    /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentCreateTime")]
    public string? PaymentCreateTime { get; init; }

    /// <summary>
    /// The time when the payment was executed. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentExecuteTime")]
    public string? PaymentExecuteTime { get; init; }

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; init; }

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string? Currency { get; init; }

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string? Intent { get; init; }

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoice")]
    public string? MerchantInvoice { get; init; }

    /// <summary>
    /// Final status of the transaction. For a successful payment, this status should be "Completed". Otherwise, the status will be "Initiated".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string? TransactionStatus { get; init; }

    /// <summary>
    /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
    /// </summary>
    [JsonProperty("verificationStatus")]
    public string? VerificationStatus { get; init; }

    /// <summary>
    /// Current status of wallet user verification. If user's verification is not performed or in progress, then the value will be "Incomplete". If user has entered wallet number, OTP, PIN and finished verification, then the value will be "Complete".
    /// </summary>
    [JsonProperty("userVerificationStatus")]
    public string? UserVerificationStatus { get; init; }

    /// <summary>
    /// Any related reference value which was passed along with the payment request. If the wallet number is passed , then it will be pre-populated in bKash's wallet number entry page.
    /// </summary>
    [JsonProperty("payerReference")]
    public string? PayerReference { get; init; }

    /// <summary>
    /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
    /// </summary>
    [JsonProperty("trxID")]
    public string? TransactionId { get; init; }
}