namespace PocketWallet.Bkash;
internal class BkashToken : IBkashToken
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly HttpClient _httpClient;
    private readonly BKashConfigurationOptions _bkashConfigurationOptions;

    private string _token = string.Empty;
    private string _refreshToken = string.Empty;
    private DateTime _tokenExpiryTime;

    internal BkashToken(
        HttpClient httpClient,
        IOptionsMonitor<BKashConfigurationOptions> bkashConfigurationOptions,
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
            if (token is null || token.Status is not null || token.Message is not null)
            {
                var exception = new Exception($"Attempt: New Token, Error Status: {token?.Status}, Error Message: {token?.Message}");
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
        if (refreshedToken is null || refreshedToken.Status is not null || refreshedToken.Message is not null)
        {
            throw new Exception(
                "Attempt: Refresh Token " +
                "Error Status: " + refreshedToken?.Status +
                "Error Message: " + refreshedToken?.Message);
        }
        _token = refreshedToken.IdToken!;
        _refreshToken = refreshedToken.RefreshToken!;
        _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

        return _token;
    }

    private async Task<TokenResponse?> CreateInitialToken()
    {
        string requestURL = $"{_bkashConfigurationOptions.BaseURL}/{CONSTANTS.TOKEN_URL}";
        var response = await _httpClient.PostAsync<TokenResponse>(
              requestURL,
              new
              {
                  app_key = _bkashConfigurationOptions.MerchantKey,
                  app_secret = _bkashConfigurationOptions.MerchantSecret
              },
              GetSecurityHeaders());

        return response.Data;
    }

    private async Task<TokenResponse?> CreateRefreshToken(string refreshToken)
    {
        string requestURL = $"{_bkashConfigurationOptions.BaseURL}/{CONSTANTS.REFRESH_TOKEN_URL}";
        var response = await _httpClient.PostAsync<TokenResponse>(
              requestURL,
              new
              {
                  app_key = _bkashConfigurationOptions.MerchantKey,
                  app_secret = _bkashConfigurationOptions.MerchantSecret,
                  refresh_token = refreshToken
              },
              GetSecurityHeaders());

        return response.Data;
    }

    private Dictionary<string, string> GetSecurityHeaders() => new()
    {
        { CONSTANTS.USERNAME_HEADER_KEY, _bkashConfigurationOptions.MerchantUserName??string.Empty },
        { CONSTANTS.PASSWORD_HEADER_KEY, _bkashConfigurationOptions.MerchantPassword??string.Empty }
    };
}
