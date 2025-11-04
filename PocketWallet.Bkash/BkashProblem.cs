namespace PocketWallet.Bkash
{
    /// <summary>
    /// Represents the problem details for all the Bkash operations.
    /// </summary>
    public class BkashProblem
    {
        /// <summary>
        /// Helps initiate problem object.
        /// </summary>
        /// <param name="statusCode">Problem status code that is received from Bkash.</param>
        /// <param name="message">Problem message received from Bkash. </param>
        private BkashProblem(string statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        /// <summary>
        /// Status code received from Bkash.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Status Message received from Bkash.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Returns an object of <see cref="BkashProblem"/>.
        /// </summary>
        /// <param name="statusCode">Problem status code that is received from Bkash.</param>
        /// <param name="message">Problem message received from Bkash. </param>
        /// <returns>Object of <see cref="BkashProblem"/>.</returns>
        public static BkashProblem Create(string statusCode, string message)
        {
            return new BkashProblem(statusCode, message);
        }
    }
}
