namespace ToDoList.Api.Configurations;

public static class AuthorizationConfig
{
    public static void AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorization();
    }
}