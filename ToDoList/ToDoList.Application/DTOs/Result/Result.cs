namespace ToDoList.Application.DTOs.Result;

public class Result
{
    public bool Succeeded { get; protected init; }
    public List<string> Errors { get; protected init; } = [];

    // Protected constructor ensures that objects cannot be created directly
    protected Result() { }
    
    public static Result Success()
    {
        return new Result { Succeeded = true };
    }
  
    public static Result Failure(List<string> errors)
    {
        return new Result { Succeeded = false, Errors = errors };
    }
}