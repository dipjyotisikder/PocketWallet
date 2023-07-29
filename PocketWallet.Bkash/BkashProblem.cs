namespace PocketWallet.Bkash
{
    public class BkashProblem
    {
        private BkashProblem(string statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public string StatusCode { get; init; }

        public string Message { get; init; }

        public static BkashProblem Create(string statusCode, string message)
        {
            return new BkashProblem(statusCode, message);
        }
    }
}
