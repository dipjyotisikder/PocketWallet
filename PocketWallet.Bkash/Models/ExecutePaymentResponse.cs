namespace PocketWallet.Bkash.Models;

public class ExecutePaymentResponse : ErrorResponse
{
    /// <summary>
    /// Unique code assigned to the API call status.
    /// </summary>
    [JsonProperty("statusCode")]
    public string? StatusCode { get; set; }

    /// <summary>
    /// Message associated with the status, explaining the status.
    /// </summary>
    [JsonProperty("statusMessage")]
    public string? StatusMessage { get; set; }

    /// <summary>
    /// bKash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
    /// </summary>
    [JsonProperty("paymentID")]
    public string? PaymentID { get; set; }

    /// <summary>
    /// The payer reference value that was provided during the create payment API call.
    /// </summary>
    [JsonProperty("payerReference")]
    public string? PayerReference { get; set; }

    /// <summary>
    /// MSISDN of the customer who performed the payment.
    /// </summary>
    [JsonProperty("customerMsisdn")]
    public string? CustomerMsisdn { get; set; }

    /// <summary>
    /// A transaction ID created after the successful completion of this payment request. This ID can be used later to get the details of this transaction.
    /// </summary>
    [JsonProperty("trxID")]
    public string? TrxID { get; set; }

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Final status of the transaction. For a successful payment, this status should be "Completed".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string? TransactionStatus { get; set; }

    /// <summary>
    /// The time when the payment was executed. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentExecuteTime")]
    public string? PaymentExecuteTime { get; set; }

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string? Intent { get; set; }

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string? MerchantInvoiceNumber { get; set; }
}