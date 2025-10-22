using PocketWallet.Bkash.Common.Abstractions;
using PocketWallet.Bkash.Common.Http;
using PocketWallet.Bkash.DependencyInjection.Options;

namespace PocketWallet.Bkash.Concretes;

/// <summary>
/// Represents Bkash authorization handling tool.
/// </summary>
internal class BkashAuthorizationHandler : IBkashAuthorizationHandler
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenProvider _tokenStorage;
    private readonly HttpClient _httpClient;
    private readonly BkashConfigurationOptions _bkashConfigurationOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="BkashAuthorizationHandler"/> class.
    /// </summary>
    /// <param name="httpClient">HTTP client configured for Bkash endpoints.</param>
    /// <param name="bkashConfigurationOptions">Bkash configuration values.</param>
    /// <param name="dateTimeProvider">Date and time provider instance.</param>
    /// <param name="tokenStorage">Token persistence store.</param>
    public BkashAuthorizationHandler(
        HttpClient httpClient,
        BkashConfigurationOptions bkashConfigurationOptions,
        IDateTimeProvider dateTimeProvider,
        ITokenProvider tokenStorage)
    {
        _httpClient = httpClient;
        _bkashConfigurationOptions = bkashConfigurationOptions;
        _dateTimeProvider = dateTimeProvider;
        _tokenStorage = tokenStorage;
    }

    /// <inheritdoc/>
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

        return Result<Dictionary<string, string>>.Create(tokenResult.Problem!);
    }

    /// <summary>
    /// Creates and stores token.
    /// </summary>
    /// <returns>A token string.</returns>
    private async Task<Result<string>> CreateToken()
    {
        if (!_tokenStorage.IsTokenAvailable)
        {
            var tokenResponse = await InitialToken();
            if (tokenResponse.IsSucceeded)
            {
                _tokenStorage.Set(
                    tokenResponse.Data!.IdToken!,
                    tokenResponse.Data.RefreshToken!,
                    _dateTimeProvider.UtcNow.AddSeconds(CONSTANTS.TOKEN_EXPIRATION_SECONDS));

                return Result<string>.Create(_tokenStorage.AccessToken);
            }

            return Result<string>.Create(tokenResponse.Problem!);
        }
        else if (!_tokenStorage.IsTokenAlive)
        {
            var refreshedTokenResponse = await RefreshToken(_tokenStorage.RefreshToken);
            if (refreshedTokenResponse.IsSucceeded)
            {
                _tokenStorage.Set(
                    refreshedTokenResponse.Data!.IdToken!,
                    refreshedTokenResponse.Data.RefreshToken!,
                    _dateTimeProvider.UtcNow.AddSeconds(CONSTANTS.TOKEN_EXPIRATION_SECONDS));

                return Result<string>.Create(_tokenStorage.AccessToken);
            }

            return Result<string>.Create(refreshedTokenResponse.Problem!);
        }

        return Result<string>.Create(_tokenStorage.AccessToken);
    }

    /// <summary>
    /// Gets initial token with network call.
    /// </summary>
    /// <returns>A token response.</returns>
    private async Task<Result<BkashTokenResponse>> InitialToken()
    {
        var response = await _httpClient.PostAsync<BkashTokenRequest, BkashTokenResponse>(
            endpoint: CONSTANTS.TOKEN_URL,
            body: new()
            {
                AppKey = _bkashConfigurationOptions.AppKey,
                AppSecret = _bkashConfigurationOptions.AppSecret
            });

        if (response.Success)
        {
            return Result<BkashTokenResponse>.Create(response.Data!);
        }

        return Result<BkashTokenResponse>.Create(
                BkashProblem.Create(
                    statusCode: response?.Data?.StatusCode!,
                    message: response?.Data?.StatusMessage!));
    }

    /// <summary>
    /// Refreshes an expired token.
    /// </summary>
    /// <param name="refreshToken">Refresh token.</param>
    /// <returns>A refreshed token response.</returns>
    private async Task<Result<BkashTokenResponse>> RefreshToken(string refreshToken)
    {
        var response = await _httpClient.PostAsync<BkashRefreshTokenRequest, BkashTokenResponse>(
              endpoint: CONSTANTS.REFRESH_TOKEN_URL,
              body: new()
              {
                  AppKey = _bkashConfigurationOptions.AppKey,
                  AppSecret = _bkashConfigurationOptions.AppSecret,
                  RefreshToken = refreshToken
              });

        if (response.Success)
        {
            return Result<BkashTokenResponse>.Create(response.Data!);
        }

        return Result<BkashTokenResponse>.Create(
               BkashProblem.Create(statusCode: response?.Data?.StatusCode!, message: response?.Data?.StatusMessage!));
    }
}
