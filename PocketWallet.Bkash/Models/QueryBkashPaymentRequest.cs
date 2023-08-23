namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents request object used to query payment information.
/// </summary>
public class QueryBkashPaymentRequest
{
    /// <summary>
    /// bKash generated paymentID provided in the response of Create Payment API.
    /// </summary>
    [JsonProperty("paymentID")]
    public string PaymentId { get; set; } = string.Empty;
}