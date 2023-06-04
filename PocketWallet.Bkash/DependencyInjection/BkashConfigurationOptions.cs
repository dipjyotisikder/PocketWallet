namespace PocketWallet.Bkash.DependencyInjection;

public class BkashConfigurationOptions
{
    private BkashConfigurationOptions() { }

    private BkashConfigurationOptions(
    string baseURL,
    string key,
    string secret,
    string userName,
    string password)
    {
        BaseURL = baseURL;
        Key = key;
        Secret = secret;
        UserName = userName;
        Password = password;
    }

    internal string? BaseURL;

    internal string? Key;

    internal string? Secret;

    internal string? UserName;

    internal string? Password;

    public static BkashConfigurationOptions CreateOptions(
    string baseURL,
    string key,
    string secret,
    string userName,
    string password)
    {
        return new BkashConfigurationOptions(
            baseURL,
            key,
            secret,
            userName,
            password
        );
    }
}
