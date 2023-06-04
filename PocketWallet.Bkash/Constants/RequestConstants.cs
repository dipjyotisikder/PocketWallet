namespace PocketWallet.Bkash.Constants;
internal static class RequestConstants
{
    internal const string TOKEN_URL = "checkout/token/grant";
    internal const string REFRESH_TOKEN_URL = "checkout/token/refresh";
    internal const string PAYMENT_CREATE_URL = "checkout/payment/create";
    internal static string PAYMENT_EXECUTE_URL(string paymentID) => $"checkout/payment/execute/{paymentID}";
}
