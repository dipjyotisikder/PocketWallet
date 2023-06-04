using Microsoft.Extensions.DependencyInjection;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        Func<BkashConfigurationOptions> configAction)
    {
        services.Configure<BkashConfigurationOptions>(x => configAction.Invoke());
        services.AddHttpClient();
        return services;
    }
}
