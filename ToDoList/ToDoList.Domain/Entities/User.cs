using Microsoft.AspNetCore.Identity;

namespace ToDoList.Domain.Entities;

public class User : IdentityUser
{
    public ICollection<Task> Tasks { get; set; }
}