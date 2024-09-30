using FluentValidation;
using FluentValidation.AspNetCore;
using ToDoList.Application.Validators.TaskRelated;

namespace ToDoList.Api.Configurations;

public static class FluentValidationConfig
{
    public static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();
        services.AddFluentValidationAutoValidation(options => {
            options.DisableDataAnnotationsValidation = true;
        }).AddFluentValidationClientsideAdapters();
    }
}