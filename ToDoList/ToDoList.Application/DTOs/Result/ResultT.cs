namespace ToDoList.Application.DTOs.Result;

public class Result<T> : Result
{
    public T? Value { get; private init; }

    private Result()
    {
    }

    public static Result<T> Success(T value)
    {
        return new Result<T> { Succeeded = true, Value = value };
    }

    public new static Result<T> Failure(List<string> errors)
    {
        return new Result<T> { Succeeded = false, Errors = errors };
    }
}