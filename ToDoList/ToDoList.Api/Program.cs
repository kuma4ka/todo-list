using DotNetEnv;
using Serilog;

namespace ToDoList.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        Env.Load();

        var configuration = CreateConfiguration();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        try
        {
            Log.Information("Starting up the application...");

            CreateHostBuilder(args).Build().Run();

            Log.Information("Application is shutting down...");
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

    private static IConfiguration CreateConfiguration()
    {
        try
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                    optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Error creating configuration");
            throw;
        }
    }
}