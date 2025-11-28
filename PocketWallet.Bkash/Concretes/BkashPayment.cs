using Knot;
using PocketWallet.Bkash.Abstraction;
using PocketWallet.Bkash.Common.Http;
using PocketWallet.Bkash.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CONSTANTS = PocketWallet.Bkash.Common.Constants.Constants;

namespace PocketWallet.Bkash.Concretes
{
    /// <summary>
    /// Class that provides the functionality to interact with Bkash Payment APIs.
    /// </summary>
    /// <remarks>
    /// Creates the Bkash Payment object to interact with Bkash.
    /// </remarks>
    /// <param name="bkashAuthorizationHandler"><see cref="BkashAuthorizationHandler"/> object created by Bkash.</param>
    /// <param name="httpClient"><see cref="HttpClient"/> object to call Bkash endpoints.</param>
    /// <param name="mapper"><see cref="IMapper"/> object to map between different object types.</param>
    internal class BkashPayment(IBkashAuthorizationHandler bkashAuthorizationHandler,
                                HttpClient httpClient,
                                IMapper mapper) : IBkashPayment
    {
        /// <inheritdoc/>
        public async Task<Result<CreatePaymentResult>> Create(CreatePaymentCommand command)
        {
            var headerResult = await bkashAuthorizationHandler.GetAuthorizationHeaders();
            if (headerResult.IsSucceeded)
            {
                var request = mapper.Map<CreatePaymentCommand, CreatePaymentRequest>(command);

                var response = await httpClient.PostAsync<CreatePaymentRequest, CreatePaymentResponse>(endpoint: CONSTANTS.PAYMENT_CREATE_URL,
                                                                                                        body: request,
                                                                                                        headers: headerResult.Data);

                if (response.Success)
                {
                    var result = mapper.Map<CreatePaymentResponse, CreatePaymentResult>(response.Data!);
                    return Result<CreatePaymentResult>.Create(result);
                }

                return Result<CreatePaymentResult>.Create(
                   BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
            }

            return Result<CreatePaymentResult>.Create(headerResult.Problem!);
        }

        /// <inheritdoc/>
        public async Task<Result<ExecutePaymentResult>> Execute(ExecutePaymentCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.PaymentId))
            {
                return Result<ExecutePaymentResult>.Create(
                         BkashProblem.Create(statusCode: CONSTANTS.APP_ERROR_RESPONSE_CODE, message: "Payment Id is invalid."));
            }

            var headerResult = await bkashAuthorizationHandler.GetAuthorizationHeaders();
            if (headerResult.IsSucceeded)
            {
                var request = mapper.Map<ExecutePaymentCommand, ExecutePaymentRequest>(command);

                var response = await httpClient.PostAsync<ExecutePaymentRequest, ExecutePaymentResponse>(endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                                                                                                          body: request,
                                                                                                          headers: headerResult.Data);

                if (response.Success)
                {
                    var result = mapper.Map<ExecutePaymentResponse, ExecutePaymentResult>(response.Data!);
                    return Result<ExecutePaymentResult>.Create(result);
                }

                return Result<ExecutePaymentResult>.Create(
                          BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
            }

            return Result<ExecutePaymentResult>.Create(headerResult.Problem!);
        }

        /// <inheritdoc/>
        public async Task<Result<QueryPaymentResult>> Query(PaymentQuery query)
        {
            var headerResult = await bkashAuthorizationHandler.GetAuthorizationHeaders();
            if (headerResult.IsSucceeded)
            {
                var request = mapper.Map<PaymentQuery, QueryPaymentRequest>(query);

                var response = await httpClient.PostAsync<QueryPaymentRequest, QueryPaymentResponse>(endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                                                                                                      body: request,
                                                                                                      headers: headerResult.Data);

                if (response.Success)
                {
                    var result = mapper.Map<QueryPaymentResponse, QueryPaymentResult>(response.Data!);
                    return Result<QueryPaymentResult>.Create(result);
                }

                return Result<QueryPaymentResult>.Create(BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
            }

            return Result<QueryPaymentResult>.Create(headerResult.Problem!);
        }

        /// <inheritdoc/>
        public async Task<Result<RefundPaymentResult>> Refund(RefundPaymentCommand command)
        {
            var headerResult = await bkashAuthorizationHandler.GetAuthorizationHeaders();
            if (headerResult.IsSucceeded)
            {
                var refundStatusResponse = await GetRefundStatus(
                    headers: headerResult.Data!,
                    request: new RefundStatusRequest
                    {
                        PaymentId = command.PaymentId,
                        TransactionId = command.TransactionId
                    }
                );

                if (refundStatusResponse.Success)
                {
                    if (refundStatusResponse.Data!.TransactionStatus == CONSTANTS.BKASH_REFUND_SUCCESS_RESPONSE_CODE)
                    {
                        var result = mapper.Map<RefundPaymentResponse, RefundPaymentResult>(refundStatusResponse.Data!);
                        return Result<RefundPaymentResult>.Create(result);
                    }
                }

                var request = mapper.Map<RefundPaymentCommand, RefundPaymentRequest>(command);

                var refundResponse = await httpClient.PostAsync<RefundPaymentRequest, RefundPaymentResponse>(endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                                                                                                              body: request,
                                                                                                              headers: headerResult.Data);

                if (refundResponse.Success)
                {
                    var result = mapper.Map<RefundPaymentResponse, RefundPaymentResult>(refundResponse.Data!);
                    return Result<RefundPaymentResult>.Create(result);
                }

                return Result<RefundPaymentResult>.Create(BkashProblem.Create(
                    statusCode: refundResponse?.Data?.StatusCode!,
                    message: refundResponse?.Data?.StatusMessage!));
            }

            return Result<RefundPaymentResult>.Create(headerResult.Problem!);
        }

        /// <summary>
        /// Queries Bkash to determine the latest refund status for a transaction.
        /// </summary>
        /// <param name="headers">Authorized headers for the Bkash call.</param>
        /// <param name="request">Refund status request payload.</param>
        /// <returns>HTTP response representing the refund status.</returns>
        private async Task<HttpResponse<RefundPaymentResponse>> GetRefundStatus(
            Dictionary<string, string> headers,
            RefundStatusRequest request)
        {
            var response = await httpClient.PostAsync<RefundStatusRequest, RefundPaymentResponse>(endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                                                                                                   body: request,
                                                                                                   headers: headers);

            return response;
        }
    }
}