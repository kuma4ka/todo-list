using ToDoList.Application.DTOs.Result;
using ToDoList.Application.DTOs.TaskRelated;

namespace ToDoList.Application.Interfaces.TaskRelated;

public interface ITaskManagementService
{
    public Task<Result> CreateTaskAsync(string creatorId, CreateTaskDto createTaskDto);
    public Task<Result> UpdateTaskAsync(UpdateTaskDto updateTaskDto);
    public Task<Result> DeleteTaskAsync(Guid taskId);
}