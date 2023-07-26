namespace PocketWallet.Bkash;

/// <summary>
/// A generic result object that indicates the result and the status of a operation.
/// </summary>
public class Result<T>
{
    /// <summary>
    /// Constractor that takes data and exceptions.
    /// </summary>
    /// <param name="data">A result object of type <typeparamref name="T"/>.</param>
    /// <param name="exceptions">Exceptions occured during the operation.</param>
    private Result(T? data = default, ICollection<Exception>? exceptions = null)
    {
        Data = data;
        Exceptions = exceptions;
        IsSucceeded = exceptions == null || !exceptions.Any();
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
    /// Exceptions that indicates if the task has problem.
    /// </summary>
    public ICollection<Exception>? Exceptions { get; set; }

    /// <summary>
    /// Result object creator with data, of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="data">A result object of type <typeparamref name="T"/>.</param>
    /// <returns>A object of type <see cref="Result{T}"/>.</returns>
    public static Result<T> Create(T data)
    {
        return new Result<T>(data, null);
    }

    /// <summary>
    /// Result object creator with exceptions.
    /// </summary>
    /// <param name="exceptions">Exceptions occured during the operation.</param>
    /// <returns>A object of type <see cref="Result{T}"/>.</returns>
    public static Result<T> Create(ICollection<Exception> exceptions)
    {
        return new Result<T>(default, exceptions);
    }

    /// <summary>
    /// Result object creator with data and exceptions.
    /// </summary>
    /// <param name="data">A result object of type <typeparamref name="T"/>.</param>
    /// <returns>A object of type <see cref="Result{T}"/>.</returns>
    public static Result<T> Create(T data, ICollection<Exception> exceptions)
    {
        return new Result<T>(data, exceptions);
    }
}

/// <summary>
/// A result object that indicates the result and the status of a operation.
/// </summary>
public class Result
{
    /// <summary>
    /// Constractor that takes exceptions.
    /// </summary>
    /// <param name="exceptions"></param>
    private Result(ICollection<Exception>? exceptions = null)
    {
        Exceptions = exceptions;
        IsSucceeded = exceptions == null || !exceptions.Any();
    }

    /// <summary>
    /// A boolean result that indicates if the task is succeeded.
    /// </summary>
    public bool IsSucceeded { get; set; }

    /// <summary>
    /// Exceptions occured during the operation.
    /// </summary>
    public ICollection<Exception>? Exceptions { get; set; }

    /// <summary>
    /// Result object creator with exceptions.
    /// </summary>
    /// <param name="exceptions">Exceptions occured during the operation.</param>
    /// <returns>A object of type <see cref="Result"/></returns>
    public static Result Create(ICollection<Exception>? exceptions = null)
    {
        return new Result(exceptions);
    }
}
