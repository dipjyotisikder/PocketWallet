using AutoMapper;
using PocketWallet.Bkash.Http;

namespace PocketWallet.Bkash.Concretes;

/// <summary>
/// Class that provides the functionality to interact with Bkash Payment APIs.
/// </summary>
internal class BkashPayment : IBkashPayment
{
    private readonly IBkashAuthorizationHandler _bkashAuthorizationHandler;
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    /// <summary>
    /// Creates the Bkash Payment object to interact with Bkash.
    /// </summary>
    /// <param name="bkashAuthorizationHandler"><see cref="BkashAuthorizationHandler"/> object created by Bkash.</param>
    /// <param name="httpClient"><see cref="HttpClient"/> object to call Bkash endpoints.</param>
    /// <param name="mapper"><see cref="IMapper"/> object.</param>
    public BkashPayment(
        IBkashAuthorizationHandler bkashAuthorizationHandler,
        HttpClient httpClient,
        IMapper mapper)
    {
        _bkashAuthorizationHandler = bkashAuthorizationHandler;
        _httpClient = httpClient;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Result<CreatePaymentResult>> Create(CreatePaymentCommand command)
    {
        var headerResult = await _bkashAuthorizationHandler.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreatePaymentRequest, CreatePaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_CREATE_URL,
                body: _mapper.Map<CreatePaymentRequest>(command),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<CreatePaymentResult>.Create(_mapper.Map<CreatePaymentResult>(response.Data!));
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

        var headerResult = await _bkashAuthorizationHandler.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecutePaymentRequest, ExecutePaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                body: _mapper.Map<ExecutePaymentRequest>(command),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<ExecutePaymentResult>.Create(_mapper.Map<ExecutePaymentResult>(response.Data!));
            }

            return Result<ExecutePaymentResult>.Create(
              BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
        }

        return Result<ExecutePaymentResult>.Create(headerResult.Problem!);
    }

    /// <inheritdoc/>
    public async Task<Result<QueryPaymentResult>> Query(PaymentQuery query)
    {
        var headerResult = await _bkashAuthorizationHandler.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<QueryPaymentRequest, QueryPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                body: _mapper.Map<QueryPaymentRequest>(query),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<QueryPaymentResult>.Create(
                    _mapper.Map<QueryPaymentResult>(response.Data!));
            }

            return Result<QueryPaymentResult>.Create(
                BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
        }

        return Result<QueryPaymentResult>.Create(headerResult.Problem!);
    }

    /// <inheritdoc/>
    public async Task<Result<RefundPaymentResult>> Refund(RefundPaymentCommand command)
    {
        var headerResult = await _bkashAuthorizationHandler.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var refundStatusResponse = await GetRefundStatus(
                headers: headerResult.Data!,
                request: new RefundStatusRequest
                {
                    PaymentId = command.PaymentId,
                    TransactionId = command.TransactionId
                });

            if (refundStatusResponse.Success)
            {
                if (refundStatusResponse.Data!.TransactionStatus == CONSTANTS.BKASH_REFUND_SUCCESS_RESPONSE_CODE)
                {
                    return Result<RefundPaymentResult>.Create(_mapper.Map<RefundPaymentResult>(refundStatusResponse.Data!));
                }
            }

            var refundResponse = await _httpClient.PostAsync<RefundPaymentRequest, RefundPaymentResponse>(
                   endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                   body: _mapper.Map<RefundPaymentRequest>(command),
                   headers: headerResult.Data);

            if (refundResponse.Success)
            {
                return Result<RefundPaymentResult>.Create(_mapper.Map<RefundPaymentResult>(refundResponse.Data!));
            }

            return Result<RefundPaymentResult>.Create(
                BkashProblem.Create(
                    statusCode: refundResponse?.Data?.StatusCode!,
                    message: refundResponse?.Data?.StatusMessage!));
        }

        return Result<RefundPaymentResult>.Create(headerResult.Problem!);
    }

    private async Task<HttpResponse<RefundPaymentResponse>> GetRefundStatus(
        Dictionary<string, string> headers,
        RefundStatusRequest request)
    {
        var response = await _httpClient.PostAsync<RefundStatusRequest, RefundPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                body: request,
                headers: headers);

        return response;
    }
}