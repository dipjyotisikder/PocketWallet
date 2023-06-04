namespace PocketWallet.Bkash.Models;

public class CreatePaymentResponse
{
    [JsonProperty("paymentID")]
    public string? PaymentID { get; set; }

    [JsonProperty("createTime")]
    public string? CreateTime { get; set; }

    [JsonProperty("orgLogo")]
    public string? OrganizationLogo { get; set; }

    [JsonProperty("orgName")]
    public string? OrganizationName { get; set; }

    [JsonProperty("transactionStatus")]
    public string? TransactionStatus { get; set; }

    [JsonProperty("amount")]
    public string? Amount { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("intent")]
    public string? Intent { get; set; }

    [JsonProperty("merchantInvoiceNumber")]
    public string? MerchantInvoiceNumber { get; set; }
}