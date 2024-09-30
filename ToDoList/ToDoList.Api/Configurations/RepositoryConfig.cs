using ToDoList.Domain.Entities;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Api.Configurations;

public static class RepositoryConfig
{
    public static void AddRepositoryConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryGeneric<User>, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}