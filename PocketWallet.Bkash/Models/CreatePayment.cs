namespace PocketWallet.Bkash.Models;

internal class CreatePayment
{
    /// <summary>
    /// Amount of money.
    /// </summary>
    [JsonProperty("amount")]
    internal decimal Amount { get; set; }

    /// <summary>
    /// Order id.
    /// </summary>
    /// <value>ORD1020069.</value>
    [JsonProperty("orderID")]
    internal string? OrderID { get; set; }

    /// <summary>
    /// Intent is what we want to create the payment for.
    /// </summary>
    /// <value>
    /// 'sale' | 'authorization'
    /// </value>
    [JsonProperty("intent")]
    internal string? Intent { get; set; }

    /// <summary>
    /// Extra information of merchant.
    /// </summary>
    [JsonProperty("merchantAssociationInfo")]
    internal string? MerchantAssociationInfo { get; set; }

    /// <summary>
    /// Currency type.
    /// </summary>
    /// <value>BDT</value>
    [JsonProperty("currency")]
    internal string Currency { get; set; } = "BDT";
}
