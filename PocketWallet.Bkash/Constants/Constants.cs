namespace PocketWallet.Bkash.Constants;

/// <summary>
/// Represents the constants.
/// </summary>
internal static class Constants
{
    /// <summary>
    /// Live base URL of Bkash Payment.
    /// </summary>
    internal const string LIVE_BASEURL = "https://tokenized.pay.bka.sh/v1.2.0-beta/";

    /// <summary>
    /// Testing/Sandbox base URL of Bkash Payment.
    /// </summary>
    internal const string SANDBOX_BASEURL = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/";

    /// <summary>
    /// Token API URL for Bkash Payment.
    /// </summary>
    internal const string TOKEN_URL = "tokenized/checkout/token/grant";

    /// <summary>
    /// Refresh Token API URL for Bkash Payment.
    /// </summary>
    internal const string REFRESH_TOKEN_URL = "tokenized/checkout/token/refresh";

    /// <summary>
    /// Payment Creation API URL for Bkash Payment.
    /// </summary>
    internal const string PAYMENT_CREATE_URL = "tokenized/checkout/create";

    /// <summary>
    /// Payment Execution API URL for Bkash Payment.
    /// </summary>
    internal const string PAYMENT_EXECUTE_URL = "tokenized/checkout/execute";

    /// <summary>
    /// Payment Status Check API URL for Bkash Payment.
    /// </summary>
    internal const string PAYMENT_STATUS_URL = "tokenized/checkout/payment/status";

    /// <summary>
    /// Payment Refund API URL for Bkash Payment.
    /// </summary>
    internal const string PAYMENT_REFUND_URL = "tokenized/checkout/payment/refund";

    /// <summary>
    /// Request header key for merchant username.
    /// </summary>
    internal const string USERNAME_HEADER_KEY = "username";

    /// <summary>
    /// Request header key for merchant password.
    /// </summary>
    internal const string PASSWORD_HEADER_KEY = "password";

    /// <summary>
    /// Request header key for merchant authorization to Bkash.
    /// </summary>
    internal const string AUTHORIZATION_HEADER_KEY = "authorization";

    /// <summary>
    /// Custom request header key for merchant authorization to Bkash.
    /// </summary>
    internal const string X_APP_KEY_HEADER_KEY = "x-app-key";

    /// <summary>
    /// Response code from Bkash API, that represents the operation was successful.
    /// </summary>
    internal const string BKASH_SUCCESS_RESPONSE_CODE = "0000";

    /// <summary>
    /// Response code from Bkash Refund API, that represents the operation was successful.
    /// </summary>
    internal const string BKASH_REFUND_SUCCESS_RESPONSE_CODE = "Completed";

    /// <summary>
    /// Response code that represents the operation was failed.
    /// </summary>
    internal const string APP_ERROR_RESPONSE_CODE = "9999";

    /// <summary>
    /// Maximum time in seconds that represents the expiration time limit.
    /// </summary>
    internal const int TOKEN_EXPIRATION_SECONDS = 3500;

    /// <summary>
    /// A code that satisfies payment mechanism with agreement.
    /// </summary>
    internal const string AGREEMENT_CODE = "0001";

    /// <summary>
    /// A code that satisfies payment mechanism without agreement.
    /// </summary>
    internal const string WITHOUT_AGREEMENT_CODE = "0011";
}
