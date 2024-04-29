namespace PocketWallet.Bkash;

/// <summary>
/// Represents input for payment information query.
/// </summary>
public class PaymentQuery
{
    /// <summary>
    /// bkash generated paymentID provided in the response of Create Payment API.
    /// </summary>
    public string PaymentId { get; set; } = string.Empty;
}