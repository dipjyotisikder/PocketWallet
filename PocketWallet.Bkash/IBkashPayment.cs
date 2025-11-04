using System.Threading.Tasks;

namespace PocketWallet.Bkash
{
    /// <summary>
    /// Bkash payment interface that helps to interact with Bkash.
    /// </summary>
    public interface IBkashPayment
    {
        /// <summary>
        /// API to provide the capability to create Bkash Payment.
        /// </summary>
        /// <param name="command">Command object for payment creation.</param>
        /// <returns>A response provided by Bkash.</returns>
        Task<Result<CreatePaymentResult>> Create(CreatePaymentCommand command);

        /// <summary>
        /// API to provide the capability to execute Bkash Payment.
        /// </summary>
        /// <param name="command">Command object for payment creation.</param>
        /// <returns>A response provided by Bkash.</returns>
        Task<Result<ExecutePaymentResult>> Execute(ExecutePaymentCommand command);

        /// <summary>
        /// API to provide the capability to query Bkash Payment.
        /// </summary>
        /// <param name="query">Query object for payment creation.</param>
        /// <returns>A response provided by Bkash.</returns>
        Task<Result<QueryPaymentResult>> Query(PaymentQuery query);

        /// <summary>
        /// API to provide the capability to refund Bkash Payment.
        /// </summary>
        /// <param name="command">Command object for payment refund.</param>
        /// <returns>A response provided by Bkash.</returns>
        Task<Result<RefundPaymentResult>> Refund(RefundPaymentCommand command);
    }
}
