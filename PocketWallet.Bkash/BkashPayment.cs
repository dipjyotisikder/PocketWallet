namespace PocketWallet.Bkash;

public class BkashPayment : IBkashPayment
{
    private readonly IBkashToken _bkashToken;
    private readonly HttpClient _httpClient;

    internal BkashPayment(
        IBkashToken bkashToken,
        HttpClient httpClient)
    {
        _bkashToken = bkashToken;
        _httpClient = httpClient;
    }

    /// <inheritdoc/>
    public async Task<Result<CreateBkashPaymentResponse>> CreatePayment(CreateBkashPayment paymentRequest)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreateBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_CREATE_URL,
                body: paymentRequest,
                headers: headerResult.Data);

            if (response.IsSuccessStatusCode)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<CreateBkashPaymentResponse>.Create(response.Data!);
                }

                return Result<CreateBkashPaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Operation: CreatePayment - Error Code:{response.Data.ErrorCode}- Error Message: {response.Data.ErrorMessage}")
                });
            }

            return Result<CreateBkashPaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<CreateBkashPaymentResponse>.Create(headerResult.Exceptions!);
    }

    /// <inheritdoc/>
    public async Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePayment executePayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecutePaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                body: executePayment,
                headers: headerResult.Data);

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

        return Result<ExecutePaymentResponse>.Create(headerResult.Exceptions!);
    }

    /// <inheritdoc/>
    public async Task<Result<QueryPaymentResponse>> QueryPayment(QueryPayment queryPayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<QueryPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                body: queryPayment,
                headers: headerResult.Data);

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

        return Result<QueryPaymentResponse>.Create(headerResult.Exceptions!);
    }
}