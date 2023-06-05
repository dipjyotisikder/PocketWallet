namespace PocketWallet.Bkash.Models;

public class ExecutePayment
{
    /// <summary>
    /// PaymentID returned in the response of Create Payment API.
    /// </summary>
    [JsonProperty("paymentID")]
    public string? PaymentID { get; set; }
}
