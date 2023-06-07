namespace PocketWallet.Bkash;

public class BkashPayment : IBkashPayment
{
    private readonly IBkashToken _bkashToken;
    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;

    internal BkashPayment(
        IBkashToken bkashToken,
        HttpClient httpClient,
        IOptionsMonitor<BkashConfigurationOptions> bkashConfigurationOptions)
    {
        _bkashToken = bkashToken;
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions.CurrentValue;
    }

    public async Task<Result<CreatePaymentResponse>> CreatePayment(CreatePayment paymentRequest)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<CreatePaymentResponse>(
                $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_CREATE_URL}",
                paymentRequest,
                headerResult.Data);

            if (response.IsSuccessStatusCode)
            {
                if (response.Response.StatusCode is "0000")
                {
                    return Result<CreatePaymentResponse>.Create(response.Response!);
                }

                return Result<CreatePaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Error Code:{response.Response.ErrorCode}- Error Message: {response.Response.ErrorMessage}") 
                });
            }

            return Result<CreatePaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.") 
            });
        }

        return Result<CreatePaymentResponse>.Create(headerResult.Exceptions);
    }

    public async Task<Result<ExecutePaymentResponse>> ExecutePayment(ExecutePayment executePayment)
    {
        var headerResult = await _bkashToken.GetAuthorizationHeaders();
        if (headerResult.IsSucceeded)
        {
            var response = await _httpClient.PostAsync<ExecutePaymentResponse>(
                $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_EXECUTE_URL}",
                executePayment,
                headerResult.Data);
            
            if (response.IsSuccessStatusCode)
            {
                if (response.Response.StatusCode is "0000")
                {
                    return Result<ExecutePaymentResponse>.Create(response.Response!);
                }

                return Result<ExecutePaymentResponse>.Create(new List<Exception> {
                    new Exception($"Bkash Error Code:{response.Response.ErrorCode}- Error Message: {response.Response.ErrorMessage}")
                });
            }

            return Result<ExecutePaymentResponse>.Create(new List<Exception> {
                new Exception("Request could not be processed.")
            });
        }

        return Result<ExecutePaymentResponse>.Create(headerResult.Exceptions);
    }
}