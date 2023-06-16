using Microsoft.Extensions.DependencyInjection;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        BKashConfigurationOptions bkashConfigurationOptions)
    {
        services.Configure<BKashConfigurationOptions>(x => x = bkashConfigurationOptions);

        services.AddHttpClient();

        services.AddSingleton<IBkashToken, BkashToken>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
