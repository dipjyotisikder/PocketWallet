namespace PocketWallet.Bkash;

/// <summary>
/// Bkash payment interface that helps to interact with Bkash.
/// </summary>
public interface IBkashPayment
{
    /// <summary>
    /// API to provide the capability to create Bkash Payment.
    /// </summary>
    /// <param name="command">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<CreatePaymentResult>> Create(CreatePaymentCommand command);

    /// <summary>
    /// API to provide the capability to execute Bkash Payment.
    /// </summary>
    /// <param name="command">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<ExecutePaymentResult>> Execute(ExecutePaymentCommand command);

    /// <summary>
    /// API to provide the capability to query Bkash Payment.
    /// </summary>
    /// <param name="query">Params for payment creation.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<QueryPaymentResult>> Query(PaymentQuery query);

    /// <summary>
    /// API to provide the capability to refund Bkash Payment.
    /// </summary>
    /// <param name="command">Object for payment refund.</param>
    /// <returns>A response provided by bkash.</returns>
    Task<Result<RefundPaymentResult>> Refund(RefundPaymentCommand command);
}
