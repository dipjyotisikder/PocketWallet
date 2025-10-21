namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents Bkash payment creation request object.
/// </summary>
internal class CreatePaymentRequest
{
    /// <summary>
    /// Any related reference value which can be passed along with the payment request. If the wallet number is passed here, then it will be pre-populated in Bkash's wallet number entry page.
    /// </summary>
    /// <example>A predefined phone/account number.</example>
    /// <remarks>Space acceptable, String required. Null is not accepted. (Its optional)</remarks>
    [JsonPropertyName("payerReference")]
    public string PayerReference { get; set; } = " ";

    /// <summary>
    /// The base URL of merchant's platform based on which Bkash will generate separate callback URLs for success, failure and canceled transactions. Bkash will send transaction verification result in these URLs based on the result.
    /// </summary>
    [JsonPropertyName("callbackURL")]
    public string CallbackURL { get; set; } = string.Empty;

    /// <summary>
    /// Amount of the payment to be made.
    /// </summary>
    [JsonPropertyName("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonPropertyName("merchantInvoiceNumber")]
    public string MerchantInvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// This parameter indicates the mode of payment. For Checkout (URL based), the value of this parameter should be "0011".
    /// </summary>
    [JsonPropertyName("mode")]
    public string Mode { get; set; } = string.Empty;

    /// <summary>
    /// Currency of the mentioned amount. Currently, only "BDT" value is acceptable.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Intent of the payment. For checkout the value should be "sale".
    /// </summary>
    [JsonPropertyName("intent")]
    public string Intent { get; set; } = string.Empty;
}