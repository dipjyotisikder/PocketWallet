namespace PocketWallet.Bkash.Models;

public class CreatePaymentResponse : ErrorResponse
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
    /// The URL of bKash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
    /// </summary>
    [JsonProperty("bkashURL")]
    public string? BkashURL { get; set; }

    /// <summary>
    /// The base URL of merchant's platform based on which bKash will generate seperate callback URLs for success, failure and cancelled transactions. bKash will send transaction verification result in these URLs based on the the result.
    /// </summary>
    [JsonProperty("callbackURL")]
    public string? CallbackURL { get; set; }

    /// <summary>
    /// The success callback URL where bkash will inform merchant about the transaction result in case of a successful transaction.
    /// </summary>
    [JsonProperty("successCallbackURL")]
    public string? SuccessCallbackURL { get; set; }

    /// <summary>
    /// The failure callback URL where bkash will inform merchant about the transaction result in case of a failed transaction.
    /// </summary>
    [JsonProperty("failureCallbackURL")]
    public string? FailureCallbackURL { get; set; }

    /// <summary>
    /// The cancelled callback URL where bkash will inform merchant about the transaction result in case of a cancelled transaction.
    /// </summary>
    [JsonProperty("cancelledCallbackURL")]
    public string? CancelledCallbackURL { get; set; }

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string? Intent { get; set; }

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentCreateTime")]
    public string? PaymentCreateTime { get; set; }

    /// <summary>
    /// Status of the initiated transaction. After Create Payment request, the value will be "Initiated".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string? TransactionStatus { get; set; }

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string? MerchantInvoiceNumber { get; set; }
}