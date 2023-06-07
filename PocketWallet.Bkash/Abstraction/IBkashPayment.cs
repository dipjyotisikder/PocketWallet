namespace PocketWallet.Bkash.Abstraction;

public interface IBkashPayment
{
    Task<Result<CreatePaymentResponse>> CreatePayment(CreatePayment paymentRequest);

    Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePayment executePayment);
}
