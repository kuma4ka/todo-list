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
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            logger.Warning("User validation failed for user: {UserId}. Errors: {Errors}", creator?.Id, errors);
            return Result.Failure(errors);
        }

        var task = new Domain.Entities.Task
        {
            Creator = creator,
            TaskTitle = createTaskDto.TaskTitle,
            Description = createTaskDto.Description,
            TaskStatus = TaskStatusEnum.InProgress
        };

        try
        {
            creator.Tasks.Add(task);
            await taskRepository.AddAsync(task);

            return Result.Success();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "An error occurred while creating the task for user: {UserId}", creator?.Id);
            return Result.Failure(["An error occurred while creating the task."]);
        }
    }

    public async Task<Result> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
    {
        return await taskExistenceStep.ExecuteIfExistsAsync(updateTaskDto.Id, async task =>
        {
            try
            {
                task.TaskTitle = updateTaskDto.Title;
                task.Description = updateTaskDto.Description;
                await taskRepository.UpdateAsync(task);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while updating the task. TaskId: {TaskId}", task.TaskId);
                throw;
            }
        });
    }

    public async Task<Result> DeleteTaskAsync(Guid taskId)
    {
        return await taskExistenceStep.ExecuteIfExistsAsync(taskId, async task =>
        {
            try
            {
                await taskRepository.DeleteAsync(task.TaskId);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An error occurred while deleting the task. TaskId: {TaskId}", task.TaskId);
                throw;
            }
        });
    }
}