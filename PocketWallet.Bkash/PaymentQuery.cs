namespace PocketWallet.Bkash.Models;

/// <summary>
/// Represents input for payment information query.
/// </summary>
public class PaymentQuery
{
    /// <summary>
    /// bKash generated paymentID provided in the response of Create Payment API.
    /// </summary>
    public string PaymentID { get; set; } = string.Empty;
}