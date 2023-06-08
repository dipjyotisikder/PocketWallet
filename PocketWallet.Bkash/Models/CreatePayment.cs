namespace PocketWallet.Bkash.Models;

public class CreatePayment
{
    /// <summary>
    /// This parameter indicates the mode of payment. For Checkout (URL based), the value of this parameter should be "0011".
    /// </summary>
    [JsonProperty("mode")]
    public string? Mode { get; set; }

    /// <summary>
    /// Any related reference value which can be passed along with the payment request. If the wallet number is passed here, then it will be pre-populated in bKash's wallet number entry page.
    /// </summary>
    [JsonProperty("payerReference")]
    public string? PayerReference { get; set; }

    /// <summary>
    /// The base URL of merchant's platform based on which bKash will generate seperate callback URLs for success, failure and cancelled transactions. bKash will send transaction verification result in these URLs based on the the result.
    /// </summary>
    [JsonProperty("callbackURL")]
    public string? CallbackURL { get; set; }

    /// <summary>
    /// This parameter is needed for aggregators and system integrators only.
    /// </summary>
    /// <remarks>
    /// The data to be used here should be in TLV format and the format is as follows - 
    /// Tag - 2 digits from the beginning. Supported tags are MI (Merchant Identifier), MW (Merchant Wallet Number) and RF (Payment Reference).Length - 2 digits after tag. Value - Remaining number of digits equal to length after length value.Example of such TLV data is - MI05MID54RF09123456789.
    /// </remarks>
    [JsonProperty("merchantAssociationInfo")]
    public string? MerchantAssociationInfo { get; set; }

    /// <summary>
    /// Amount of the payment to be made.
    /// </summary>
    [JsonProperty("amount")]
    public string? Amount { get; set; }

    /// <summary>
    /// Currency of the mentioned amount. Currently, only "BDT" value is acceptable.
    /// </summary>
    [JsonProperty("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Intent of the payment. For checkout the value should be "sale".
    /// </summary>
    [JsonProperty("intent")]
    public string? Intent { get; set; }

    /// <summary>
    /// Unique invoice number used at merchant side for this specific payment.
    /// </summary>
    [JsonProperty("merchantInvoiceNumber")]
    public string? MerchantInvoiceNumber { get; set; }
}