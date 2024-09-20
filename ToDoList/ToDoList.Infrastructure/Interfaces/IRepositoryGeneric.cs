namespace ToDoList.Infrastructure.Interfaces;

public interface IRepositoryGeneric<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetByIdAsync(string id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
}