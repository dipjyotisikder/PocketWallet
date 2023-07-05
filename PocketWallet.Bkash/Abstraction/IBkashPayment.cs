namespace PocketWallet.Bkash.Abstraction;

public interface IBkashPayment
{
    /// <summary>
    /// API to provide the capability to create Bkash Payment.
    /// </summary>
    /// <param name="paymentRequest">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<CreateBkashPaymentResponse>> CreatePayment(CreateBkashPayment paymentRequest);

    /// <summary>
    /// API to provide the capability to execute Bkash Payment.
    /// </summary>
    /// <param name="executePayment">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePayment executePayment);

    /// <summary>
    /// API to provide the capability to query Bkash Payment.
    /// </summary>
    /// <param name="queryPayment">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<QueryPaymentResponse>> QueryPayment(QueryPayment queryPayment);
}
