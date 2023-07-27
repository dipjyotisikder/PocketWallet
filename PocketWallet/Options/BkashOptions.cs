namespace PocketWallet.Options
{
    public class BkashOptions
    {
        public string MerchantUserName { get; set; } = string.Empty;
        public string MerchantPassword { get; set; } = string.Empty;
        public string AppKey { get; set; } = string.Empty;
        public string AppSecret { get; set; } = string.Empty;
        public bool ProductionMode { get; set; }
    }
}
