namespace ToDoList.Application.DTOs.TaskRelated;

public record UpdateTaskDto(Guid Id, string Title, string Description);