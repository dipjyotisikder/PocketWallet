namespace PocketWallet.Bkash;

internal class BkashPayment : IBkashPayment
{
    private readonly IBkashToken _bkashToken;
    private readonly HttpClient _httpClient;

    public BkashPayment(
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

            if (response.Success)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<CreateBkashPaymentResponse>.Create(response.Data!);
                }

                return Result<CreateBkashPaymentResponse>.Create(new List<Exception> {
                    new Exception(string.Format("Bkash CreatePayment Failed | Error Code: {0} | Error Message: {1}", response.Data.ErrorCode, response.Data.ErrorMessage))
                });
            }

            return Result<CreateBkashPaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<CreateBkashPaymentResponse>.Create(headerResult.Exceptions!);
    }

    /// <inheritdoc/>
    public async Task<Result<ExecuteBkashPaymentResponse>> ExecutePayment(ExecuteBkashPayment executePayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecuteBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_EXECUTE_URL,
                body: executePayment,
                headers: headerResult.Data);

            if (response.Success)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<ExecuteBkashPaymentResponse>.Create(response.Data!);
                }

                return Result<ExecuteBkashPaymentResponse>.Create(new List<Exception> {
                    new Exception(string.Format("Bkash ExecutePayment Failed | Error Code: {0} | Error Message: {1}", response.Data.ErrorCode, response.Data.ErrorMessage))
                });
            }

            return Result<ExecuteBkashPaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<ExecuteBkashPaymentResponse>.Create(headerResult.Exceptions!);
    }

    /// <inheritdoc/>
    public async Task<Result<QueryBkashPaymentResponse>> QueryPayment(QueryBkashPayment queryPayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<QueryBkashPaymentResponse>(
                endpoint: CONSTANTS.PAYMENT_STATUS_URL,
                body: queryPayment,
                headers: headerResult.Data);

            if (response.Success)
            {
                if (response.Data!.StatusCode is CONSTANTS.SUCCESS_RESPONSE_CODE)
                {
                    return Result<QueryBkashPaymentResponse>.Create(response.Data!);
                }

                return Result<QueryBkashPaymentResponse>.Create(new List<Exception> {
                    new Exception(string.Format("Bkash QueryPayment Failed | Error Code: {0} | Error Message: {1}", response.Data.ErrorCode, response.Data.ErrorMessage))
                });
            }

            return Result<QueryBkashPaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<QueryBkashPaymentResponse>.Create(headerResult.Exceptions!);
    }
}