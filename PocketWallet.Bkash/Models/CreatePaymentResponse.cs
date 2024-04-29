namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents payment creation response object.
/// </summary>
internal class CreatePaymentResponse : BaseBkashResponse
{
    /// <summary>
    /// bkash generated payment ID for this payment creation request. This payment ID can be used later to track down the payment and in other payment related APIs.
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentId { get; init; } = string.Empty;

    /// <summary>
    /// The URL of bkash where the customer should be forwarded to enter his wallet number, OTP and wallet PIN.
    /// </summary>
    [JsonProperty("bkashURL")]
    public string BkashURL { get; init; } = string.Empty;

    /// <summary>
    /// The base URL of merchant's platform based on which bkash will generate separate callback URLs for success, failure and canceled transactions. bkash will send transaction verification result in these URLs based on the result.
    /// </summary>
    [JsonProperty("callbackURL")]
    public string CallbackURL { get; init; } = string.Empty;

    /// <summary>
    /// The success callback URL where bkash will inform merchant about the transaction result in case of a successful transaction.
    /// </summary>
    [JsonProperty("successCallbackURL")]
    public string SuccessCallbackURL { get; init; } = string.Empty;

    /// <summary>
    /// The failure callback URL where bkash will inform merchant about the transaction result in case of a failed transaction.
    /// </summary>
    [JsonProperty("failureCallbackURL")]
    public string FailureCallbackURL { get; init; } = string.Empty;

    /// <summary>
    /// The canceled callback URL where bkash will inform merchant about the transaction result in case of a canceled transaction.
    /// </summary>
    [JsonProperty("cancelledCallbackURL")]
    public string CanceledCallbackURL { get; init; } = string.Empty;

    /// <summary>
    /// Amount of the payment transaction.
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; init; } = string.Empty;

    /// <summary>
    /// Intent for the payment transaction. For checkout, the value will be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string Intent { get; init; } = string.Empty;

    /// <summary>
    /// Currency of the mentioned amount. Currently only "BDT" value is supported.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; init; } = string.Empty;

    /// <summary>
    /// The payment creation time. Format is** **- "yyyy-MM-dd'T'HH:mm:ss 'GMT'Z".
    /// </summary>
    [JsonProperty("paymentCreateTime")]
    public string PaymentCreateTime { get; init; } = string.Empty;

    /// <summary>
    /// Status of the initiated transaction. After Create Payment request, the value will be "Initiated".
    /// </summary>
    [JsonProperty("transactionStatus")]
    public string TransactionStatus { get; init; } = string.Empty;

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string MerchantInvoiceNumber { get; init; } = string.Empty;
}