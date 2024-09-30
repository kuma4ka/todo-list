using ToDoList.Api.CustomMiddlewares;
using ToDoList.Application.Helpers;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Interfaces.TaskRelated;
using ToDoList.Application.Interfaces.UserRelated;
using ToDoList.Application.Services;
using ToDoList.Application.Services.TaskRelated;
using ToDoList.Application.Wrappers;
using ToDoList.Infrastructure.Interfaces;
using ToDoList.Infrastructure.Repositories;

namespace ToDoList.Api.Configurations;

public static class AppServiceConfig
{
    public static void AddApplicationServiceConfiguration(this IServiceCollection services)
    {
        // Scoped services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskManagementService, TaskManagementService>();
        services.AddScoped<IEntityExistenceStep<Domain.Entities.Task>, EntityExistenceStep<Domain.Entities.Task>>();
        services.AddScoped(typeof(IUserManager<>), typeof(UserManagerWrapper<>));
        services.AddScoped(typeof(IRoleManager<>), typeof(RoleManagerWrapper<>));
        services.AddScoped(typeof(IRepositoryGeneric<>), typeof(GenericRepository<>));
        
        services.AddScoped<ValidationBehaviorMiddleware>();

        // Singleton services
        services.AddSingleton<IJwtService, JwtService>();
    }
}