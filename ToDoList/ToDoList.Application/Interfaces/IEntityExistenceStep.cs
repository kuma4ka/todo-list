using ToDoList.Application.DTOs.Result;

namespace ToDoList.Application.Interfaces;

public interface IEntityExistenceStep<T>
{
    Task<Result> ExecuteIfExistsAsync(Guid id, Func<T, Task> action);
    Task<Result<T>> GetIfExistsAsync(Guid id);
}