namespace PocketWallet.Bkash.Models;

public class QueryPayment
{
    /// <summary>
    /// bKash generated paymentID provided in the response of Create Payment API.
    /// </summary>
    [JsonProperty("paymentID")]
    public string? PaymentID { get; set; }
}