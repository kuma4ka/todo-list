using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.DTOs.Result;
using ToDoList.Application.ResultExtensions;

namespace ToDoList.Api.Extensions;

public static class TaskExtensions
{
    public static async Task<IActionResult> WithResultHandling<T>(this Task<T> task, string successMessage)
    {
        try
        {
            var data = await task;
            var result = Result<T>.Success(data);
            return result.ToResponse(successMessage);
        }
        catch (Exception ex)
        {
            var result = Result<T>.Failure([ex.Message]);
            return result.ToResponse("An error occurred while processing the request.");
        }
    }
}