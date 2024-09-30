using ToDoList.Api.Configurations;
using ToDoList.Api.CustomMiddlewares;

namespace ToDoList.Api;

public class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersioningConfiguration();
        services.AddApplicationServiceConfiguration();
        services.AddAuthorizationConfiguration();
        services.AddControllerConfiguration();
        services.AddDbContextConfiguration(Configuration);
        services.AddFluentValidationConfiguration();
        services.AddIdentityConfiguration();
        services.AddJwtConfiguration(Configuration);
        services.AddLoggingConfiguration(Configuration);
        services.AddRepositoryConfiguration();
        services.AddSwaggerConfiguration();
        services.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ValidationBehaviorMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}