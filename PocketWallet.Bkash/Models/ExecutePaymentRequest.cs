namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents payment execution request object.
/// </summary>
internal class ExecutePaymentRequest
{
    /// <summary>
    /// PaymentID returned in the response of Create Payment API.
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentId { get; set; } = string.Empty;
}
