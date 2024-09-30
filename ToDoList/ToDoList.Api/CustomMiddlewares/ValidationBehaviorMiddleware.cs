using FluentValidation.Results;

namespace ToDoList.Api.CustomMiddlewares;

public class ValidationBehaviorMiddleware(ILogger<ValidationBehaviorMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Items["__FluentValidationErrors__"] is ValidationResult { IsValid: false } validationErrors)
        {
            logger.LogWarning("Validation failed for request {RequestPath}. Errors: {Errors}",
                context.Request.Path,
                validationErrors.Errors.Select(e => e.ErrorMessage));

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                errors = validationErrors.Errors.Select(e => e.ErrorMessage)
            });
            return;
        }

        await next(context);
    }
}