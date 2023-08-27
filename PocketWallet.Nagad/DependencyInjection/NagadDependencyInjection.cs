using Microsoft.Extensions.DependencyInjection;
using PocketWallet.Nagad.Abstraction;
using PocketWallet.Nagad.Concretes;
using PocketWallet.Nagad.DependencyInjection.Options;

namespace PocketWallet.Nagad.DependencyInjection;
/// <summary>
/// Adds service dependencies.
/// </summary>
public static class NagadDependencyInjection
{
    /// <summary>
    /// Adds nagad specific dependencies.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    /// <param name="optionsAction">Action that helps to fill nagad options.</param>
    /// <returns></returns>
    public static IServiceCollection AddNagad(this IServiceCollection services, Action<NagadConfigurationOptions> optionsAction)
    {
        var options = new NagadConfigurationOptions
        {
            ProductionMode = false
        };

        optionsAction.Invoke(options);

        services.AddSingleton(x => options);

        /*services.AddHttpClient<INagadAuthorizationHandler, NagadAuthorizationHandler>(x =>
        {
            x.BaseAddress = new Uri(options.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, options.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, options.MerchantPassword);
        });

        services.AddHttpClient<INagadPayment, NagadPayment>(x =>
        {
            x.BaseAddress = new Uri(options.BaseURL, UriKind.Absolute);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            x.DefaultRequestHeaders.Add(CONSTANTS.USERNAME_HEADER_KEY, options.MerchantUserName);
            x.DefaultRequestHeaders.Add(CONSTANTS.PASSWORD_HEADER_KEY, options.MerchantPassword);
        });*/

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<ITokenStorage, TokenStorage>();

        return services;
    }
}
