﻿namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents bkash payment creation request object.
/// </summary>
internal class CreatePaymentRequest
{
    /// <summary>
    /// Any related reference value which can be passed along with the payment request. If the wallet number is passed here, then it will be pre-populated in bkash's wallet number entry page.
    /// </summary>
    /// <example>A predefined phone/account number.</example>
    /// <remarks>Space acceptable, String required. Null is not accepted. (Its optional)</remarks>
    [JsonProperty("payerReference")]
    public string PayerReference { get; set; } = " ";

    /// <summary>
    /// The base URL of merchant's platform based on which bkash will generate separate callback URLs for success, failure and canceled transactions. bkash will send transaction verification result in these URLs based on the result.
    /// </summary>
    [JsonProperty("callbackURL")]
    public string CallbackURL { get; set; } = string.Empty;

    /// <summary>
    /// Amount of the payment to be made.
    /// </summary>
    [JsonProperty("amount")]
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string MerchantInvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    /// This parameter indicates the mode of payment. For Checkout (URL based), the value of this parameter should be "0011".
    /// </summary>
    [JsonProperty("mode")]
    public string Mode { get; set; } = string.Empty;

    /// <summary>
    /// Currency of the mentioned amount. Currently, only "BDT" value is acceptable.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Intent of the payment. For checkout the value should be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string Intent { get; set; } = string.Empty;
}