namespace PocketWallet.Bkash;

/// <summary>
/// Represents the problem details for all the bkash operations.
/// </summary>
public class BkashProblem
{
    /// <summary>
    /// Helps initiate problem object.
    /// </summary>
    /// <param name="statusCode">Problem status code that is received from bkash.</param>
    /// <param name="message">Problem message received from bkash. </param>
    private BkashProblem(string statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    /// <summary>
    /// Status code received from Bkash.
    /// </summary>
    public string StatusCode { get; init; }

    /// <summary>
    /// Status Message received from Bkash.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Returns an object of <see cref="BkashProblem"/>.
    /// </summary>
    /// <param name="statusCode">Problem status code that is received from bkash.</param>
    /// <param name="message">Problem message received from bkash. </param>
    /// <returns>Object of <see cref="BkashProblem"/>.</returns>
    public static BkashProblem Create(string statusCode, string message)
    {
        return new BkashProblem(statusCode, message);
    }
}
