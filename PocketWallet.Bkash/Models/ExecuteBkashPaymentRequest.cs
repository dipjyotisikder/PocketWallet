namespace PocketWallet.Bkash.Models;

internal class ExecuteBkashPaymentRequest
{
    /// <summary>
    /// PaymentID returned in the response of Create Payment API.
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentID { get; set; } = string.Empty;
}
