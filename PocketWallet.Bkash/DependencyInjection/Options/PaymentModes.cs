namespace PocketWallet.Bkash.DependencyInjection.Options
{
    /// <summary>
    /// Represents payment modes. IE. With Agreement and without agreement.
    /// </summary>
    public enum PaymentModes
    {
        /// <summary>
        /// Represents payment mode that helps user to be in an agreement with merchant and removes pin providing step.
        /// </summary>
        WithAgreement = 0001,

        /// <summary>
        /// Represents payment mode that proceeds without agreement.
        /// </summary>
        WithoutAgreement = 0011
    }
}
