namespace PocketWallet.Bkash.Models;

public class CreatePayment
{
    /// <summary>
    /// Amount of money.
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Order id.
    /// </summary>
    /// <value>ORD1020069.</value>
    [JsonProperty("orderID")]
    public string? OrderID { get; set; }

    /// <summary>
    /// Intent is what we want to create the payment for.
    /// </summary>
    /// <value>
    /// 'sale' | 'authorization'
    /// </value>
    [JsonProperty("intent")]
    public string? Intent { get; set; }

    /// <summary>
    /// Extra information of merchant.
    /// </summary>
    [JsonProperty("merchantAssociationInfo")]
    public string? MerchantAssociationInfo { get; set; }

    /// <summary>
    /// Currency type.
    /// </summary>
    /// <value>BDT</value>
    [JsonProperty("currency")]
    public string Currency { get; set; } = "BDT";
}
