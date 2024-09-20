namespace ToDoList.Infrastructure.Interfaces;

public interface ITaskRepository : IRepositoryGeneric<Domain.Entities.Task>
{
    Task DeleteAsync(Guid id);
}