namespace PocketWallet.Bkash.Constants;

internal static class Constants
{
    internal const string LIVE_BASEURL = "https://tokenized.pay.bka.sh/v1.2.0-beta/";
    internal const string SANDBOX_BASEURL = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/";

    internal const string TOKEN_URL = "tokenized/checkout/token/grant";
    internal const string REFRESH_TOKEN_URL = "tokenized/checkout/token/refresh";
    internal const string PAYMENT_CREATE_URL = "tokenized/checkout/create";
    internal const string PAYMENT_EXECUTE_URL = "tokenized/checkout/execute";
    internal const string PAYMENT_STATUS_URL = "tokenized/checkout/payment/status";
    internal const string PAYMENT_REFUND_URL = "tokenized/checkout/payment/refund";

    internal const string ACCEPT_HEADER_KEY = "accept";
    internal const string CONTENT_TYPE_HEADER_KEY = "content-type";
    internal const string USERNAME_HEADER_KEY = "username";
    internal const string PASSWORD_HEADER_KEY = "password";
    internal const string AUTHORIZATION_HEADER_KEY = "authorization";
    internal const string X_APP_KEY_HEADER_KEY = "x-app-key";

    internal const string JSON_HEADER_VALUE = "application/json";

    internal const string BKASH_SUCCESS_RESPONSE_CODE = "0000";
    internal const string BKASH_REFUND_SUCCESS_RESPONSE_CODE = "Completed";
    internal const string APP_ERROR_RESPONSE_CODE = "9999";

    internal const int TOKEN_EXPIRATION_SECONDS = 3500;

    internal const string AGREEMENT_CODE = "0001";
    internal const string WITHOUT_AGREEMENT_CODE = "0011";
}
