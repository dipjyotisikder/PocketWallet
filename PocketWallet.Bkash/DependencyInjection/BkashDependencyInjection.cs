using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        BkashConfigurationOptions bkashConfigurationOptions)
    {
        services.Configure<BkashConfigurationOptions>(x => x = bkashConfigurationOptions);

        services.AddHttpClient<HttpClient>(x =>
        {
            x.BaseAddress = new Uri(bkashConfigurationOptions.BaseURL);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddSingleton<IBkashToken, BkashToken>();
        services.AddSingleton<IBkashPayment, BkashPayment>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
