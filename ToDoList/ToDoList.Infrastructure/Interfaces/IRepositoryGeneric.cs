namespace ToDoList.Infrastructure.Interfaces;

public interface IRepositoryGeneric<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}