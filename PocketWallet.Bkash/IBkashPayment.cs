namespace PocketWallet.Bkash;

public interface IBkashPayment
{
    /// <summary>
    /// API to provide the capability to create Bkash Payment.
    /// </summary>
    /// <param name="request">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<CreatePaymentResponse>> CreatePayment(CreatePaymentCommand request);

    /// <summary>
    /// API to provide the capability to execute Bkash Payment.
    /// </summary>
    /// <param name="request">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePaymentCommand request);

    /// <summary>
    /// API to provide the capability to query Bkash Payment.
    /// </summary>
    /// <param name="queryPayment">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<QueryBkashPaymentResponse>> QueryPayment(QueryBkashPaymentRequest queryPayment);
}
