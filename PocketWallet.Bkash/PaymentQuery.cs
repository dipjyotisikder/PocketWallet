namespace PocketWallet.Bkash.Models;

public class PaymentQuery
{
    /// <summary>
    /// bKash generated paymentID provided in the response of Create Payment API.
    /// </summary>
    public string PaymentID { get; set; } = string.Empty;
}