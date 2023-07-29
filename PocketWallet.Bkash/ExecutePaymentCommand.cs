namespace PocketWallet.Bkash;

/// <summary>
/// Represents input object for executing the payment to accomplishment.
/// </summary>
public class ExecutePaymentCommand
{
    /// <summary>
    /// PaymentID returned in the response of Create Payment API.
    /// </summary>
    public string PaymentID { get; set; } = string.Empty;
}
