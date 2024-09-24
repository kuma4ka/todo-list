using FluentValidation;
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
    ITaskRepository taskRepository,
    IEntityExistenceStep<Domain.Entities.Task> taskExistenceStep,
    IValidator<User?> userValidator,
    ILogger logger) : ITaskManagementService
{
    public async Task<Result> CreateTaskAsync(User? creator, CreateTaskDto createTaskDto)
    {
        var validationResult = await userValidator.ValidateAsync(creator);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult
                .Errors.Select(e => e.ErrorMessage).ToList());
        }

        var task = new Domain.Entities.Task
        {
            Creator = creator,
            TaskTitle = createTaskDto.TaskTitle,
            Description = createTaskDto.Description,
            TaskStatus = TaskStatusEnum.InProgress
        };

        creator.Tasks.Add(task);

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