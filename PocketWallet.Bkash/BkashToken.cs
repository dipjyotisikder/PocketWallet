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

    public async Task<Dictionary<string, string>> GetSecurityTokenHeaders()
    {
        var token = await CreateToken();
        var headers = new Dictionary<string, string>()
        {
            { "username", _bkashConfigurationOptions.UserName ?? string.Empty },
            { "password", _bkashConfigurationOptions.Password ?? string.Empty },
            { "authorization", token },
            { "x-app-key", _bkashConfigurationOptions.Key ?? string.Empty}
        };

        return headers;
    }

    private async Task<string> CreateToken()
    {
        if (string.IsNullOrWhiteSpace(_token))
        {
            var token = await CreateInitialToken();
            if (token is null || token.Status is not null || token.Msg is not null)
            {
                throw new Exception(
                    "Attemp: New Token " +
                    "Error Status: " + token?.Status +
                    "Error Message: " + token?.Msg);
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
        if (refreshedToken is null || refreshedToken.Status is not null || refreshedToken.Msg is not null)
        {
            throw new Exception(
                "Attemp: Refresh Token " +
                "Error Status: " + refreshedToken?.Status +
                "Error Message: " + refreshedToken?.Msg);
        }
        _token = refreshedToken.IdToken!;
        _refreshToken = refreshedToken.RefreshToken!;
        _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

        return _token;
    }

    private async Task<TokenResponse?> CreateInitialToken()
    {
        string requestURL = $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.TOKEN_URL}";
        var response = await _httpClient.PostAsync<TokenResponse>(
              requestURL,
              new
              {
                  app_key = _bkashConfigurationOptions.Key,
                  app_secret = _bkashConfigurationOptions.Secret
              },
              GetSecurityHeaders());

        return response.Response;
    }

    private async Task<TokenResponse?> CreateRefreshToken(string refreshToken)
    {
        string requestURL = $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.REFRESH_TOKEN_URL}";
        var response = await _httpClient.PostAsync<TokenResponse>(
              requestURL,
              new
              {
                  app_key = _bkashConfigurationOptions.Key,
                  app_secret = _bkashConfigurationOptions.Secret,
                  refresh_token = refreshToken
              },
              GetSecurityHeaders());

        return response.Response;
    }

    private Dictionary<string, string> GetSecurityHeaders() => new()
    {
        {"username", _bkashConfigurationOptions.UserName??string.Empty },
        {"password", _bkashConfigurationOptions.Password??string.Empty }
    };
}
