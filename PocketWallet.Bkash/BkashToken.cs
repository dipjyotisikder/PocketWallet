namespace PocketWallet.Bkash;
internal class BkashToken : IBkashToken
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;

    private string _token = string.Empty;
    private string _refreshToken = string.Empty;
    private DateTime _tokenExpiryTime;

    public BkashToken(
        HttpClient httpClient,
        BkashConfigurationOptions bkashConfigurationOptions,
        IDateTimeProvider dateTimeProvider)
    {
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Dictionary<string, string>>> GetAuthorizationHeaders()
    {
        var tokenResult = await CreateToken();

        if (tokenResult.IsSucceeded)
        {
            return Result<Dictionary<string, string>>.Create(new Dictionary<string, string>()
            {
                { CONSTANTS.AUTHORIZATION_HEADER_KEY, tokenResult.Data! },
                { CONSTANTS.X_APP_KEY_HEADER_KEY, _bkashConfigurationOptions.AppKey },
            });
        }

        return Result<Dictionary<string, string>>.Create(tokenResult.Exceptions!);
    }

    private async Task<Result<string>> CreateToken()
    {
        if (string.IsNullOrWhiteSpace(_token))
        {
            var tokenResponse = await CreateInitialToken();
            if (!tokenResponse.IsSucceeded)
            {
                return Result<string>.Create(tokenResponse.Exceptions!);
            }

            _token = tokenResponse.Data!.IdToken!;
            _refreshToken = tokenResponse.Data!.RefreshToken!;
            _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

            return Result<string>.Create(_token);
        }

        var result = DateTime.Compare(_tokenExpiryTime, _dateTimeProvider.UtcNow);
        if (result > 0)
        {
            return Result<string>.Create(_token);
        }

        var refreshedTokenResponse = await CreateRefreshToken(_refreshToken);
        if (!refreshedTokenResponse.IsSucceeded)
        {
            return Result<string>.Create(refreshedTokenResponse.Exceptions!);
        }

        _token = refreshedTokenResponse.Data!.IdToken!;
        _refreshToken = refreshedTokenResponse.Data!.RefreshToken!;
        _tokenExpiryTime = _dateTimeProvider.UtcNow.AddSeconds(3500);

        return Result<string>.Create(_token);
    }

    private async Task<Result<BkashTokenResponse>> CreateInitialToken()
    {
        var response = await _httpClient.PostAsync<BkashTokenResponse>(
            endpoint: CONSTANTS.TOKEN_URL,
            body: new { app_key = _bkashConfigurationOptions.AppKey, app_secret = _bkashConfigurationOptions.AppSecret });

        if (response.Success)
        {
            return Result<BkashTokenResponse>.Create(response.Data!);
        }

        return Result<BkashTokenResponse>.Create(
            new List<Exception> {
                new Exception(response.Response)
            });
    }

    private async Task<Result<BkashTokenResponse>> CreateRefreshToken(string refreshToken)
    {
        var response = await _httpClient.PostAsync<BkashTokenResponse>(
              endpoint: CONSTANTS.REFRESH_TOKEN_URL,
              body: new { app_key = _bkashConfigurationOptions.AppKey, app_secret = _bkashConfigurationOptions.AppSecret, refresh_token = refreshToken });

        if (response.Success)
        {
            return Result<BkashTokenResponse>.Create(response.Data!);
        }

        return Result<BkashTokenResponse>.Create(
            new List<Exception> {
                new Exception(response.Response)
            });
    }
}
