namespace PocketWallet.Bkash.Constants;
internal static class Constants
{
    internal const string TOKEN_URL = "tokenized/checkout/token/grant";
    internal const string REFRESH_TOKEN_URL = "tokenized/checkout/token/refresh";
    internal const string PAYMENT_CREATE_URL = "tokenized/checkout/create";
    internal const string PAYMENT_EXECUTE_URL = "tokenized/checkout/execute";
    internal const string PAYMENT_STATUS_URL = "tokenized/checkout/payment/status";

    internal const string ACCEPT_HEADER_KEY = "accept";
    internal const string CONTENT_TYPE_HEADER_KEY = "content-type";
    internal const string USERNAME_HEADER_KEY = "username";
    internal const string PASSWORD_HEADER_KEY = "password";
    internal const string AUTHORIZATION_HEADER_KEY = "authorization";
    internal const string X_APP_KEY_HEADER_KEY = "x-app-key";

    internal const string JSON_HEADER_VALUE = "application/json";

    internal const string SUCCESS_RESPONSE_CODE = "0000";

    internal const string LIVE_BASEURL = "https://pay.bka.sh/";
    internal const string SANDBOX_BASEURL = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/";
}
