using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoList.Infrastructure.Data;

namespace ToDoList.Api.Configurations;

public static class DbContextConfig
{
    public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?.Replace("<DB_HOST>", Environment.GetEnvironmentVariable("DB_HOST") ?? string.Empty)
            .Replace("<DB_NAME>", Environment.GetEnvironmentVariable("DB_NAME") ?? string.Empty)
            .Replace("<DB_USERNAME>", Environment.GetEnvironmentVariable("DB_USERNAME") ?? string.Empty)
            .Replace("<DB_PASSWORD>", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? string.Empty);

        Log.Information("Database Connection String: {ConnectionString}", connectionString);

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("ToDoList.Infrastructure")));
    }
}