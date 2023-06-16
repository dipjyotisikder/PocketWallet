namespace PocketWallet.Bkash;

public class BkashPayment : IBkashPayment
{
    private readonly IBkashToken _bkashToken;
    private readonly HttpClient _httpClient;
    private readonly BKashConfigurationOptions _bkashConfigurationOptions;

    internal BkashPayment(
        IBkashToken bkashToken,
        HttpClient httpClient,
        IOptionsMonitor<BKashConfigurationOptions> bkashConfigurationOptions)
    {
        _bkashToken = bkashToken;
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions.CurrentValue;
    }

    /// <inheritdoc/>
    public async Task<Result<CreatePaymentResponse>> CreatePayment(CreatePayment paymentRequest)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreatePaymentResponse>(
                $"{_bkashConfigurationOptions.BaseURL}/{CONSTANTS.PAYMENT_CREATE_URL}",
                paymentRequest,
                headerResult.Data);

            if (response.IsSuccessStatusCode)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<CreatePaymentResponse>.Create(response.Data!);
                }

                return Result<CreatePaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Operation: CreatePayment - Error Code:{response.Data.ErrorCode}- Error Message: {response.Data.ErrorMessage}")
                });
            }

            return Result<CreatePaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<CreatePaymentResponse>.Create(headerResult.Exceptions);
    }

    /// <inheritdoc/>
    public async Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePayment executePayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecutePaymentResponse>(
                $"{_bkashConfigurationOptions.BaseURL}/{CONSTANTS.PAYMENT_EXECUTE_URL}",
                executePayment,
                headerResult.Data);

            if (response.IsSuccessStatusCode)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<ExecutePaymentResponse>.Create(response.Data!);
                }

                return Result<ExecutePaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Operation: ExecutePayment - Error Code:{response.Data.ErrorCode}- Error Message: {response.Data.ErrorMessage}")
                });
            }

            return Result<ExecutePaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<ExecutePaymentResponse>.Create(headerResult.Exceptions);
    }

    /// <inheritdoc/>
    public async Task<Result<QueryPaymentResponse>> QueryPayment(QueryPayment queryPayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<QueryPaymentResponse>(
                $"{_bkashConfigurationOptions.BaseURL}/{CONSTANTS.PAYMENT_EXECUTE_URL}",
                queryPayment,
                headerResult.Data);

            if (response.IsSuccessStatusCode)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<QueryPaymentResponse>.Create(response.Data!);
                }

                return Result<QueryPaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Operation: QueryPayment - Error Code:{response.Data.ErrorCode} - Error Message: {response.Data.ErrorMessage}")
                });
            }

            return Result<QueryPaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<QueryPaymentResponse>.Create(headerResult.Exceptions);
    }
}