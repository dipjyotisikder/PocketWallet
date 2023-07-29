namespace PocketWallet.Bkash;

/// <summary>
/// A generic result object that indicates the result and the status of a operation.
/// </summary>
public class Result<T>
{
    /// <summary>
    /// Constructor that takes data and Problem.
    /// </summary>
    /// <param name="data">A result object of type <typeparamref name="T"/>.</param>
    private Result(T data)
    {
        Data = data;
        IsSucceeded = true;
    }

    /// <summary>
    /// Constructor that takes data and Problem.
    /// </summary>
    /// <param name="problem">A result object of type <typeparamref name="T"/>.</param>
    private Result(BkashProblem problem)
    {
        Problem = problem;
        IsSucceeded = false;
    }

    /// <summary>
    /// A result object of type <typeparamref name="T"/>.
    /// </summary>
    public T? Data { get; private set; }

    /// <summary>
    /// A boolean result that indicates if the task is succeeded.
    /// </summary>
    public bool IsSucceeded { get; private set; }

    /// <summary>
    /// Problem that indicates if the task has problem.
    /// </summary>
    public BkashProblem? Problem { get; private set; }

    /// <summary>
    /// Result object creator with data, of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="data">A result object of type <typeparamref name="T"/>.</param>
    /// <returns>A object of type <see cref="Result{T}"/>.</returns>
    public static Result<T> Create(T data)
    {
        return new Result<T>(data);
    }

    /// <summary>
    /// Result object creator with Problem.
    /// </summary>
    /// <param name="problem">BkashProblem occurred during the operation.</param>
    /// <returns>A object of type <see cref="Result{T}"/>.</returns>
    public static Result<T> Create(BkashProblem problem)
    {
        return new Result<T>(problem);
    }
}

/// <summary>
/// A result object that indicates the result and the status of a operation.
/// </summary>
public class Result
{
    private Result() => IsSucceeded = true;

    /// <summary>
    /// Constructor that takes Problem.
    /// </summary>
    /// <param name="problem">BkashProblem occurred during the operation.</param>
    private Result(BkashProblem problem)
    {
        Problem = problem;
        IsSucceeded = false;
    }

    /// <summary>
    /// A boolean result that indicates if the task is succeeded.
    /// </summary>
    public bool IsSucceeded { get; private set; }

    /// <summary>
    /// Problem occurred during the operation.
    /// </summary>
    public BkashProblem? Problem { get; private set; }

    /// <summary>
    /// Result object creator with problem.
    /// </summary>
    /// <returns>A object of type <see cref="Result"/>.</returns>
    public static Result Create()
    {
        return new Result();
    }

    /// <summary>
    /// Result object creator with problem.
    /// </summary>
    /// <param name="problem">Problem occurred during the operation.</param>
    /// <returns>A object of type <see cref="Result"/>.</returns>
    public static Result Create(BkashProblem problem)
    {
        return new Result(problem);
    }
}
