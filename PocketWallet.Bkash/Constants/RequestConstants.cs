namespace PocketWallet.Bkash.Constants;
internal static class RequestConstants
{
    internal const string TOKEN_URL = "tokenized/token/grant";
    internal const string REFRESH_TOKEN_URL = "tokenized/token/refresh";
    internal const string PAYMENT_CREATE_URL = "tokenized/payment/create";
    internal const string PAYMENT_EXECUTE_URL = "tokenized/payment/execute";

    internal const string USERNAME_HEADER_KEY = "username";
    internal const string PASSWORD_HEADER_KEY = "password";
    internal const string AUTHORIZATION_HEADER_KEY = "authorization";
    internal const string X_APP_KEY_HEADER_KEY = "x-app-key";
}
