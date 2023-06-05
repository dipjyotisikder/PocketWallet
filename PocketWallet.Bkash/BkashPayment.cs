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

    public async Task<CreatePaymentResponse> CreatePayment(CreatePayment paymentRequest)
    {
        var headers = await _bkashToken.GetSecurityTokenHeaders();
        var response = await _httpClient.PostAsync<CreatePaymentResponse>(
            $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_CREATE_URL}",
            paymentRequest,
            headers);

        return response.Response;
    }

    public async Task<ExecutePaymentResponse> ExecutePayment(ExecutePayment executePayment)
    {
        var headers = await _bkashToken.GetAuthorizationHeaders();
        var response = await _httpClient.PostAsync<ExecutePaymentResponse>(
            $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_EXECUTE_URL}",
            executePayment,
            headers);

        return response.Response;
    }
}