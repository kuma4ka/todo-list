using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Domain.Enums;

namespace ToDoList.Domain.Entities;

[Table("Tasks")]
public class Task
{
    [Key]
    public Guid TaskId { get; init; }
    
    [Required]
    [MaxLength(100)]
    public required string TaskTitle { get; set; }
    
    [Required]
    [MaxLength(500)]
    public required string Description { get; set; }
    
    [Required]
    public required TaskStatusEnum TaskStatus { get; set; }
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    [Required]
    public required User Creator { get; init; }
}