using ToDoList.Application.DTOs.Result;
using ToDoList.Application.DTOs.TaskRelated;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Interfaces.TaskRelated;

public interface ITaskManagementService
{
    public Task<Result> CreateTaskAsync(User? user, CreateTaskDto createTaskDto);
    public Task<Result> UpdateTaskAsync(UpdateTaskDto updateTaskDto);
    public Task<Result> DeleteTaskAsync(Guid taskId);
}