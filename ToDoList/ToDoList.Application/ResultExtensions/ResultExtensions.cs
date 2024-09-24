using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.DTOs.Result;

namespace ToDoList.Application.ResultExtensions;

public static class ResultExtensions
{
    public static IActionResult ToResponse<T>(this Result<T> result, string successMessage)
    {
        if (result.Succeeded)
        {
            return new OkObjectResult(new { Message = successMessage, Data = result.Value });
        }
        return new BadRequestObjectResult(new { result.Errors });
    }

    public static IActionResult ToResponse(this Result result, string successMessage)
    {
        if (result.Succeeded)
        {
            return new OkObjectResult(new { Message = successMessage });
        }
        return new BadRequestObjectResult(new { result.Errors });
    }
}