using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext context) : ITaskRepository
{
    public async Task<Domain.Entities.Task?> GetByIdAsync(Guid id)
    {
        return await context.Tasks.FindAsync(id);
    }

    public async Task AddAsync(Domain.Entities.Task newTask)
    {
        ArgumentNullException.ThrowIfNull(newTask, nameof(newTask));

        await context.Tasks.AddAsync(newTask);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Task task)
    {
        ArgumentNullException.ThrowIfNull(task, nameof(task));

        context.Tasks.Update(task);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await context.Tasks.FindAsync(id);
        if (task != null)
        {
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }
    }
}