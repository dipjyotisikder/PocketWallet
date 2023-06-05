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
        var payload = new
        {
            amount = paymentRequest.Amount,
            intent = paymentRequest.Intent,
            currency = paymentRequest.Currency,
            merchantInvoiceNumber = paymentRequest.OrderID,
            merchantAssociationInfo = paymentRequest.MerchantAssociationInfo
        };

        var headers = await _bkashToken.GetAuthorizationHeaders();
        var response = await _httpClient.PostAsync<CreatePaymentResponse>(
            $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_CREATE_URL}",
            payload,
            headers);

        return response.Response;
    }

    public async Task<ExecutePaymentResponse> ExecutePayment(string paymentId)
    {
        var headers = await _bkashToken.GetAuthorizationHeaders();
        var response = await _httpClient.PostAsync<ExecutePaymentResponse>(
            $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.PAYMENT_EXECUTE_URL(paymentId)}",
            new { },
            headers);

        return response.Response;
    }
}