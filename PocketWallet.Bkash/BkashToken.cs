using Microsoft.Extensions.Options;
using PocketWallet.Bkash.Constants;
using PocketWallet.Bkash.DependencyInjection;

namespace PocketWallet.Bkash;

internal class BkashToken : IBkashToken
{
    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;

    internal BkashToken(HttpClient httpClient,
        IOptionsMonitor<BkashConfigurationOptions> bkashConfigurationOptions)
    {
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions.CurrentValue;
    }

    public async Task<TokenResponse?> CreateInitialToken()
    {
        string requestURL = $"{_bkashConfigurationOptions.BaseURL}/{RequestConstants.INITIAL_TOKEN_REQUEST_URL}";
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

    private Dictionary<string, string> GetSecurityHeaders() => new()
    {
            {"username", _bkashConfigurationOptions.UserName??string.Empty },
            {"password", _bkashConfigurationOptions.Password??string.Empty }
        };
}
