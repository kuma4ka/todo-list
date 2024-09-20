using Serilog;
using ToDoList.Application.DTOs.Result;
using ToDoList.Application.DTOs.TaskRelated;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Interfaces.TaskRelated;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Enums;
using ToDoList.Infrastructure.Interfaces;

namespace ToDoList.Application.Services.TaskRelated;

public class TaskManagementService(
    IRepositoryGeneric<User> userRepository,
    ITaskRepository taskRepository,
    IEntityExistenceStep<Domain.Entities.Task> taskExistenceStep,
    ILogger logger) : ITaskManagementService
{
    public async Task<Result> CreateTaskAsync(string creatorId, CreateTaskDto createTaskDto)
    {
        var creator = await userRepository.GetByIdAsync(creatorId);

        if (creator is null)
        {
            logger.Error("Failed to create task: Creator with ID {CreatorId} not found", creatorId);
            return Result.Failure(["Creator not found"]);
        }

        var task = new Domain.Entities.Task
        {
            Creator = creator,
            TaskTitle = createTaskDto.TaskTitle,
            Description = createTaskDto.Description,
            TaskStatus = TaskStatusEnum.InProgress
        };

        await taskRepository.AddAsync(task);
        return Result.Success();
    }

    public async Task<Result> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
    {
        return await taskExistenceStep.ExecuteIfExistsAsync(updateTaskDto.Id, async task =>
        {
            task.TaskTitle = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;

            await taskRepository.UpdateAsync(task);
        });
    }

    public async Task<Result> DeleteTaskAsync(Guid taskId)
    {
        return await taskExistenceStep.ExecuteIfExistsAsync(taskId,
            async task => { await taskRepository.DeleteAsync(task.TaskId); });
    }
}