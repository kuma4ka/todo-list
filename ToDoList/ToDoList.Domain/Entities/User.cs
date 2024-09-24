using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Domain.Entities;

public class User : IdentityUser
{
    [Required]
    public ICollection<Task> Tasks { get; init; } = new List<Task>();
}