using Microsoft.Extensions.DependencyInjection;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        BkashConfigurationOptions bkashConfigurationOptions)
    {
        services.Configure<BkashConfigurationOptions>(x => x = bkashConfigurationOptions);

        services.AddHttpClient();

        services.AddSingleton<IBkashToken, BkashToken>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
