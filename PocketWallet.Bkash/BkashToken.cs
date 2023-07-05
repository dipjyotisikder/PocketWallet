namespace PocketWallet.Bkash;
internal class BkashToken : IBkashToken
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;

    private string _token = string.Empty;
    private string _refreshToken = string.Empty;
    private DateTime _tokenExpiryTime;

    internal BkashToken(
        HttpClient httpClient,
        IOptionsMonitor<BkashConfigurationOptions> bkashConfigurationOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions.CurrentValue;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders()
    {
        try
        {
            var token = await CreateToken();
            var headers = new Dictionary<string, string>()
            {
                { CONSTANTS.USERNAME_HEADER_KEY, _bkashConfigurationOptions.MerchantUserName ?? string.Empty },
                { CONSTANTS.PASSWORD_HEADER_KEY, _bkashConfigurationOptions.MerchantPassword ?? string.Empty },
                { CONSTANTS.AUTHORIZATION_HEADER_KEY, token },
                { CONSTANTS.X_APP_KEY_HEADER_KEY, _bkashConfigurationOptions.MerchantKey ?? string.Empty}
            };

            return Result<Dictionary<string, string>>.Create(headers);
        }
        catch (Exception e)
        {
            return Result<Dictionary<string, string>>.Create(new List<Exception> { e });
        }
    }

    private async Task<string> CreateToken()
    {
        if (string.IsNullOrWhiteSpace(_token))
        {
            var token = await CreateInitialToken();
            if (token is null || token.StatusCode is not null || token.StatusMessage is not null)
            {
                var exception = new Exception($"Attempt: New Token, Error Status: {token?.StatusCode}, Error Message: {token?.StatusMessage}");
                throw exception;
            }
            _token = token.IdToken!;
            _refreshToken = token.RefreshToken!;
            _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

            return _token;
        }

        var result = DateTime.Compare(_tokenExpiryTime, _dateTimeProvider.UtcNow);
        if (result > 0)
        {
            return _token;
        }

        var refreshedToken = await CreateRefreshToken(_refreshToken);
        if (refreshedToken is null || refreshedToken.StatusCode is not null || refreshedToken.StatusMessage is not null)
        {
            throw new Exception(
                "Attempt: Refresh Token " +
                "Error Status: " + refreshedToken?.StatusCode +
                "Error Message: " + refreshedToken?.StatusMessage);
        }
        _token = refreshedToken.IdToken!;
        _refreshToken = refreshedToken.RefreshToken!;
        _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

        return _token;
    }

    private async Task<TokenResponse?> CreateInitialToken()
    {
        var response = await _httpClient.PostAsync<TokenResponse>(
            endpoint: CONSTANTS.TOKEN_URL,
            body: new { app_key = _bkashConfigurationOptions.MerchantKey, app_secret = _bkashConfigurationOptions.MerchantSecret },
            headers: GetSecurityHeaders());

        return response.Data;
    }

    private async Task<TokenResponse?> CreateRefreshToken(string refreshToken)
    {
        var response = await _httpClient.PostAsync<TokenResponse>(
              endpoint: CONSTANTS.REFRESH_TOKEN_URL,
              body: new { app_key = _bkashConfigurationOptions.MerchantKey, app_secret = _bkashConfigurationOptions.MerchantSecret, refresh_token = refreshToken },
              headers: GetSecurityHeaders());

        return response.Data;
    }

    private Dictionary<string, string> GetSecurityHeaders() => new()
    {
        { CONSTANTS.USERNAME_HEADER_KEY, _bkashConfigurationOptions.MerchantUserName??string.Empty },
        { CONSTANTS.PASSWORD_HEADER_KEY, _bkashConfigurationOptions.MerchantPassword??string.Empty }
    };
}
