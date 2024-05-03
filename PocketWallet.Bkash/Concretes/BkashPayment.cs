using AutoMapper;
using PocketWallet.Bkash.DependencyInjection.Options;
using PocketWallet.Bkash.Http;

namespace PocketWallet.Bkash.Concretes;

/// <summary>
/// Class that provides the functionality to interact with Bkash Payment APIs.
/// </summary>
internal class BkashPayment : IBkashPayment
{
    private readonly IBkashAuthorizationHandler _bkashAuthorizationHandler;
    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;
    private readonly IMapper _mapper;

    /// <summary>
    /// Creates the Bkash Payment object to interact with Bkash.
    /// </summary>
    /// <param name="bkashAuthorizationHandler"><see cref="BkashAuthorizationHandler"/> object created by Bkash.</param>
    /// <param name="httpClient"><see cref="HttpClient"/> object to call Bkash endpoints.</param>
    /// <param name="bkashConfigurationOptions"><see cref="BkashConfigurationOptions"/> object.</param>
    /// <param name="mapper"><see cref="IMapper"/> object.</param>
    public BkashPayment(
        IBkashAuthorizationHandler bkashAuthorizationHandler,
        HttpClient httpClient,
        BkashConfigurationOptions bkashConfigurationOptions,
        IMapper mapper)
    {
        _bkashAuthorizationHandler = bkashAuthorizationHandler;
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Result<CreatePaymentResult>> Create(CreatePaymentCommand command)
    {
        if (_bkashConfigurationOptions.PaymentMode == PaymentModes.WithoutAgreement
            && command.Mode != CONSTANTS.WITHOUT_AGREEMENT_CODE)
        {
            return Result<CreatePaymentResult>.Create(
               BkashProblem.Create(statusCode: CONSTANTS.APP_ERROR_RESPONSE_CODE, message: "Payment mode is invalid."));
        }

        if (_bkashConfigurationOptions.PaymentMode == PaymentModes.WithAgreement
            && command.Mode != CONSTANTS.AGREEMENT_CODE)
        {
            return Result<CreatePaymentResult>.Create(
               BkashProblem.Create(statusCode: CONSTANTS.APP_ERROR_RESPONSE_CODE, message: "Payment mode is invalid."));
        }

        if (command.PayerReference == null)
        {
            return Result<CreatePaymentResult>.Create(
               BkashProblem.Create(statusCode: CONSTANTS.APP_ERROR_RESPONSE_CODE, message: "Payer reference is invalid."));
        }

        var headerResult = await _bkashAuthorizationHandler.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreateBkashPaymentRequest, CreateBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_CREATE_URL,
                body: _mapper.Map<CreateBkashPaymentRequest>(command),
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
            var response = await _httpClient.PostAsync<ExecuteBkashPaymentRequest, ExecuteBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                body: _mapper.Map<ExecuteBkashPaymentRequest>(command),
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
            var response = await _httpClient.PostAsync<QueryBkashPaymentRequest, QueryBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                body: _mapper.Map<QueryBkashPaymentRequest>(query),
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

            var refundResponse = await _httpClient.PostAsync<RefundBkashPaymentRequest, RefundBkashPaymentResponse>(
                   endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                   body: _mapper.Map<RefundBkashPaymentRequest>(command),
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

    private async Task<HttpResponse<RefundBkashPaymentResponse>> GetRefundStatus(
        Dictionary<string, string> headers,
        RefundStatusRequest request)
    {
        var response = await _httpClient.PostAsync<RefundStatusRequest, RefundBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_REFUND_URL,
                body: request,
                headers: headers);

        return response;
    }
}