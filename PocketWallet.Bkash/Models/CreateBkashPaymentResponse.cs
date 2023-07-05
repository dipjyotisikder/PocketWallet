namespace PocketWallet.Bkash.Models;

public class CreateBkashPaymentResponse : ErrorResponse
{
    /// <summary>
    /// Unique code assigned to the API call status.
    /// </summary>
    [JsonProperty("statusCode")]
    public string? StatusCode { get; init; }

    /// <summary>
    /// Message associated with the status, explaining the status.
    /// </summary>
    [JsonProperty("statusMessage")]
    public string? StatusMessage { get; init; }

    /// <summary>
    /// bKash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
    /// </summary>
    [JsonProperty("paymentID")]
    public string? PaymentID { get; init; }

    /// <summary>
    /// The URL of bKash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
    /// </summary>
    [JsonProperty("bkashURL")]
    public string? BkashURL { get; init; }

    /// <summary>
    /// The base URL of merchant's platform based on which bKash will generate separate callback URLs for success, failure and canceled transactions. bKash will send transaction verification result in these URLs based on the result.
    /// </summary>
    [JsonProperty("callbackURL")]
    public string? CallbackURL { get; init; }

    /// <summary>
    /// The success callback URL where bkash will inform merchant about the transaction result in case of a successful transaction.
    /// </summary>
    [JsonProperty("successCallbackURL")]
    public string? SuccessCallbackURL { get; init; }

    /// <summary>
    /// The failure callback URL where bkash will inform merchant about the transaction result in case of a failed transaction.
    /// </summary>
    [JsonProperty("failureCallbackURL")]
    public string? FailureCallbackURL { get; init; }

    /// <summary>
    /// The canceled callback URL where bkash will inform merchant about the transaction result in case of a canceled transaction.
    /// </summary>
    [JsonProperty("cancelledCallbackURL")]
    public string? CancelledCallbackURL { get; init; }

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; init; }

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string? Intent { get; init; }

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string? Currency { get; init; }

    /// <summary>
    /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentCreateTime")]
    public string? PaymentCreateTime { get; init; }

    /// <summary>
    /// Status of the initiated transaction. After Create Payment request, the value will be "Initiated".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string? TransactionStatus { get; init; }

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string? MerchantInvoiceNumber { get; init; }
}