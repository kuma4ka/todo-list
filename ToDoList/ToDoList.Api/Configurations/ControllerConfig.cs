namespace ToDoList.Api.Configurations;

public static class ControllerConfig
{
    public static void AddControllerConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
    }
}