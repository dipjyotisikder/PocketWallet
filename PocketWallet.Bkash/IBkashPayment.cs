namespace PocketWallet.Bkash;

public interface IBkashPayment
{
    /// <summary>
    /// API to provide the capability to create Bkash Payment.
    /// </summary>
    /// <param name="request">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<CreatePaymentResponse>> Create(CreatePaymentCommand request);

    /// <summary>
    /// API to provide the capability to execute Bkash Payment.
    /// </summary>
    /// <param name="request">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<ExecutePaymentResponse>> Execute(ExecutePaymentCommand request);

    /// <summary>
    /// API to provide the capability to query Bkash Payment.
    /// </summary>
    /// <param name="query">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<PaymentQueryResponse>> Query(PaymentQuery query);
}
