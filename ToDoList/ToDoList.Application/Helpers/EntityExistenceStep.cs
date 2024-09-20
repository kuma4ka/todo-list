using ToDoList.Application.DTOs.Result;
using ToDoList.Application.Interfaces;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Application.Helpers;

public class EntityExistenceStep<T>(IRepositoryGeneric<T> repository) : IEntityExistenceStep<T>
{
    public async Task<Result> ExecuteIfExistsAsync(Guid id, Func<T, Task> action)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result.Failure([$"{typeof(T).Name} not found"]);
        }

        await action(entity);
        return Result.Success();
    }

    public async Task<Result<T>> GetIfExistsAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        
        return entity is null ? 
            Result<T>.Failure([$"{typeof(T).Name} not found"]) : Result<T>.Success(entity);
    }
}