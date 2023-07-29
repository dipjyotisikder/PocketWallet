using AutoMapper;
using PocketWallet.Bkash.Http;

namespace PocketWallet.Bkash.Concretes;

/// <summary>
/// Class that provides the functionality to interact with Bkash Payment APIs.
/// </summary>
internal class BkashPayment : IBkashPayment
{
    private readonly IBkashToken _bkashToken;
    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;
    private readonly IMapper _mapper;

    /// <summary>
    /// Creates the Bkash Payment object to interact with Bkash.
    /// </summary>
    /// <param name="bkashToken"><see cref="BkashToken"/> object created by Bkash.</param>
    /// <param name="httpClient"><see cref="HttpClient"/> object to call Bkash endpoints.</param>
    /// <param name="bkashConfigurationOptions"><see cref="BkashConfigurationOptions"/> object.</param>
    public BkashPayment(
        IBkashToken bkashToken,
        HttpClient httpClient,
        BkashConfigurationOptions bkashConfigurationOptions,
        IMapper mapper)
    {
        _bkashToken = bkashToken;
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Result<CreatePaymentResponse>> Create(CreatePaymentCommand request)
    {
        if (_bkashConfigurationOptions.PaymentMode == PaymentModes.WithoutAgreement
            && request.Mode != CONSTANTS.WITHOUT_AGREEMENT_CODE)
        {
            return Result<CreatePaymentResponse>.Create(
               BkashProblem.Create(statusCode: "9999", message: "Payment mode is invalid."));
        }

        if (_bkashConfigurationOptions.PaymentMode == PaymentModes.WithAgreement
            && request.Mode != CONSTANTS.AGREEMENT_CODE)
        {
            return Result<CreatePaymentResponse>.Create(
               BkashProblem.Create(statusCode: "9999", message: "Payment mode is invalid."));
        }

        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreateBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_CREATE_URL,
                body: _mapper.Map<CreateBkashPaymentRequest>(request),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<CreatePaymentResponse>.Create(_mapper.Map<CreatePaymentResponse>(response.Data!));
            }

            return Result<CreatePaymentResponse>.Create(
                BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
        }

        return Result<CreatePaymentResponse>.Create(headerResult.Problem!);
    }

    /// <inheritdoc/>
    public async Task<Result<ExecutePaymentResponse>> Execute(ExecutePaymentCommand request)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecuteBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                body: _mapper.Map<ExecuteBkashPaymentRequest>(request),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<ExecutePaymentResponse>.Create(_mapper.Map<ExecutePaymentResponse>(response.Data!));
            }

            return Result<ExecutePaymentResponse>.Create(
              BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
        }

        return Result<ExecutePaymentResponse>.Create(headerResult.Problem!);
    }

    /// <inheritdoc/>
    public async Task<Result<PaymentQueryResponse>> Query(PaymentQuery request)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<QueryBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                body: _mapper.Map<QueryBkashPaymentRequest>(request),
                headers: headerResult.Data);

            if (response.Success)
            {
                return Result<PaymentQueryResponse>.Create(
                    _mapper.Map<PaymentQueryResponse>(response.Data!));
            }

            return Result<PaymentQueryResponse>.Create(
                BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
        }

        return Result<PaymentQueryResponse>.Create(headerResult.Problem!);
    }
}