using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

public class Task
{
    public Guid TaskId { get; set; }
    public required string CreatorId { get; set; }
    public required string TaskTitle { get; set; }
    public required string Description { get; set; }
    public required TaskStatusEnum TaskStatus { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public required User Creator { get; init; }
}