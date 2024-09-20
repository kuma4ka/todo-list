namespace ToDoList.Application.DTOs.UserRelated;

public record UserDto(string Id, string UserName, string Email, IEnumerable<string> Roles);