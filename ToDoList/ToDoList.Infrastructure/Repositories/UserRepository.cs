using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Data;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IRepositoryGeneric<User>
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async System.Threading.Tasks.Task AddAsync(User newUser)
    {
        ArgumentNullException.ThrowIfNull(newUser, nameof(newUser));

        await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(User user)
    {
        ArgumentNullException.ThrowIfNull(user, nameof(user));

        context.Users.Update(user);
        await context.SaveChangesAsync();
    }
}