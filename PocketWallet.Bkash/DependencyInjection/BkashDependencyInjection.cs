using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        BkashConfigurationOptions bkashConfigurationOptions)
    {
        services.AddSingleton(bkashConfigurationOptions);

        services.AddHttpClient<IBkashToken, BkashToken>(x =>
        {
            x.BaseAddress = new Uri(bkashConfigurationOptions.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, bkashConfigurationOptions.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, bkashConfigurationOptions.MerchantPassword);
        });

        services.AddHttpClient<IBkashPayment, BkashPayment>(x =>
        {
            x.BaseAddress = new Uri(bkashConfigurationOptions.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, bkashConfigurationOptions.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, bkashConfigurationOptions.MerchantPassword);
        });

        /*services.AddScoped<IBkashToken, BkashToken>();
        services.AddScoped<IBkashPayment, BkashPayment>();*/

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }
}
