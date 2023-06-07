namespace PocketWallet.Bkash;

public class Result<T>
{
    private Result(T? data = default, ICollection<Exception>? exceptions = null)
    {
        Data = data;
        Exceptions = exceptions;
        IsSucceeded = exceptions == null || !exceptions.Any();
    }

    public T? Data { get; private set; }

    public bool IsSucceeded { get; private set; }

    public ICollection<Exception>? Exceptions { get; set; }

    public static Result<T> Create(T data, ICollection<Exception>? exceptions = null)
    {
        return new Result<T>(data, exceptions);
    }

    public static Result<T> Create(ICollection<Exception>? exceptions = null)
    {
        return new Result<T>(default, exceptions);
    }
}

public class Result
{
    private Result(ICollection<Exception>? exceptions = null)
    {
        Exceptions = exceptions;
        IsSucceeded = exceptions == null || !exceptions.Any();
    }

    public bool IsSucceeded { get; set; }

    public ICollection<Exception>? Exceptions { get; set; }

    public static Result Create(ICollection<Exception>? exceptions = null)
    {
        return new Result(exceptions);
    }
}
