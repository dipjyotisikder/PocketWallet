namespace PocketWallet.Nagad;

/// <summary>
/// Represents the problem details for all the Nagad operations.
/// </summary>
public class NagadProblem
{
    /// <summary>
    /// Helps initiate problem object.
    /// </summary>
    /// <param name="statusCode">Problem status code that is received from Nagad.</param>
    /// <param name="message">Problem message received from Nagad. </param>
    private NagadProblem(string statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    /// <summary>
    /// Status code received from Nagad.
    /// </summary>
    public string StatusCode { get; init; }

    /// <summary>
    /// Status Message received from Nagad.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Returns an object of <see cref="NagadProblem"/>.
    /// </summary>
    /// <param name="statusCode">Problem status code that is received from Nagad.</param>
    /// <param name="message">Problem message received from Nagad. </param>
    /// <returns>Object of <see cref="NagadProblem"/>.</returns>
    public static NagadProblem Create(string statusCode, string message)
    {
        return new NagadProblem(statusCode, message);
    }
}
