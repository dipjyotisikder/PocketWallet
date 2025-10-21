namespace PocketWallet.Bkash.Common.Constants
{
    /// <summary>
    /// Represents the constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Live base URL of Bkash Payment.
        /// </summary>
        public const string LIVE_BASEURL = "https://tokenized.pay.bka.sh/v1.2.0-beta/";

        /// <summary>
        /// Testing/Sandbox base URL of Bkash Payment.
        /// </summary>
        public const string SANDBOX_BASEURL = "https://tokenized.sandbox.bka.sh/v1.2.0-beta/";

        /// <summary>
        /// Token API URL for Bkash Payment.
        /// </summary>
        public const string TOKEN_URL = "tokenized/checkout/token/grant";

        /// <summary>
        /// Refresh Token API URL for Bkash Payment.
        /// </summary>
        public const string REFRESH_TOKEN_URL = "tokenized/checkout/token/refresh";

        /// <summary>
        /// Payment Creation API URL for Bkash Payment.
        /// </summary>
        public const string PAYMENT_CREATE_URL = "tokenized/checkout/create";

        /// <summary>
        /// Payment Execution API URL for Bkash Payment.
        /// </summary>
        public const string PAYMENT_EXECUTE_URL = "tokenized/checkout/execute";

        /// <summary>
        /// Payment Status Check API URL for Bkash Payment.
        /// </summary>
        public const string PAYMENT_STATUS_URL = "tokenized/checkout/payment/status";

        /// <summary>
        /// Payment Refund API URL for Bkash Payment.
        /// </summary>
        public const string PAYMENT_REFUND_URL = "tokenized/checkout/payment/refund";

        /// <summary>
        /// Request header key for merchant username.
        /// </summary>
        public const string USERNAME_HEADER_KEY = "username";

        /// <summary>
        /// Request header key for merchant password.
        /// </summary>
        public const string PASSWORD_HEADER_KEY = "password";

        /// <summary>
        /// Request header key for merchant authorization to Bkash.
        /// </summary>
        public const string AUTHORIZATION_HEADER_KEY = "authorization";

        /// <summary>
        /// Custom request header key for merchant authorization to Bkash.
        /// </summary>
        public const string X_APP_KEY_HEADER_KEY = "x-app-key";

        /// <summary>
        /// Response code from Bkash API, that represents the operation was successful.
        /// </summary>
        public const string BKASH_SUCCESS_RESPONSE_CODE = "0000";

        /// <summary>
        /// Response code from Bkash Refund API, that represents the operation was successful.
        /// </summary>
        public const string BKASH_REFUND_SUCCESS_RESPONSE_CODE = "Completed";

        /// <summary>
        /// Response code that represents the operation was failed.
        /// </summary>
        public const string APP_ERROR_RESPONSE_CODE = "9999";

        /// <summary>
        /// Maximum time in seconds that represents the expiration time limit.
        /// </summary>
        public const int TOKEN_EXPIRATION_SECONDS = 3500;

        /// <summary>
        /// A code that satisfies payment mechanism with agreement.
        /// </summary>
        public const string AGREEMENT_CODE = "0001";

        /// <summary>
        /// A code that satisfies payment mechanism without agreement.
        /// </summary>
        public const string WITHOUT_AGREEMENT_CODE = "0011";

        /// <summary>
        /// Currency type of Bangladesh.
        /// </summary>
        public const string BDT = "BDT";

        /// <summary>
        /// Intent of merchant for Url Based Checkout.
        /// </summary>
        public const string SALE = "sale";
    }
}
