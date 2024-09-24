using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.DTOs.TaskRelated;
using ToDoList.Application.Interfaces.TaskRelated;
using ToDoList.Application.Interfaces.UserRelated;
using ToDoList.Application.ResultExtensions;
using ToDoList.Domain.Entities;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TaskController(
    ITaskManagementService taskManagementService,
    IUserManager<User> userManager
    ) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId is null)
        {
            return BadRequest("Creator ID not found in the token.");
        }

        var user = await userManager.FindByIdAsync(userId);

        var result = await taskManagementService.CreateTaskAsync(user, createTaskDto);
        
        return result.ToResponse("Task created successfully");
    }
    
    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] UpdateTaskDto updateTaskDto)
    {
        var result = await taskManagementService.UpdateTaskAsync(updateTaskDto);
        return result.ToResponse("Task updated successfully");
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid taskId)
    {
        var result = await taskManagementService.DeleteTaskAsync(taskId);
        return result.ToResponse("Task deleted successfully");
    }
}