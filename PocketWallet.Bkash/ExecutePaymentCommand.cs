namespace PocketWallet.Bkash;

public class ExecutePaymentCommand
{
    /// <summary>
    /// PaymentID returned in the response of Create Payment API.
    /// </summary>
    public string PaymentID { get; set; } = string.Empty;
}
