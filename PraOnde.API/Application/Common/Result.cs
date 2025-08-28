namespace PraOnde.API.Application.Common;

public class Result<T> where T : class
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    
    private Result(T? value, bool isSuccess, string? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T> Success(T value) => new Result<T>(value, true, null);
    public static Result<T> Fail(string error) => new Result<T>(default, false, error);
}