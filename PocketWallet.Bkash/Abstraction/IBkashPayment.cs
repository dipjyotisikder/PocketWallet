namespace PocketWallet.Bkash.Abstraction;

public interface IBkashPayment
{
    Task<CreatePaymentResponse> CreatePayment(CreatePayment paymentRequest);

    Task<ExecutePaymentResponse> ExecutePayment(ExecutePayment executePayment);
}
