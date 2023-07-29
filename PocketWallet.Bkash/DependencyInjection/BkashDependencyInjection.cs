using Microsoft.Extensions.DependencyInjection;
using PocketWallet.Bkash.Concretes;
using PocketWallet.Bkash.DependencyInjection.Options;
using PocketWallet.Bkash.Providers;
using System.Net.Http.Headers;

namespace PocketWallet.Bkash.DependencyInjection;

public static class BkashDependencyInjection
{
    public static IServiceCollection AddBkash(
        this IServiceCollection services,
        Action<BkashConfigurationOptions> optionsCall)
    {
        var options = new BkashConfigurationOptions
        {
            ProductionMode = false
        };

        optionsCall.Invoke(options);

        services.AddSingleton(x => options);

        services.AddHttpClient<IBkashToken, BkashToken>(x =>
        {
            x.BaseAddress = new Uri(options.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, options.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, options.MerchantPassword);
        });

        services.AddHttpClient<IBkashPayment, BkashPayment>(x =>
        {
            x.BaseAddress = new Uri(options.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, options.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, options.MerchantPassword);
        });

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSwaggerGenNewtonsoftSupport();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<ITokenStorage, TokenStorage>();

        return services;
    }
}
