namespace ToDoList.Application.DTOs.UserRelated;

public record UserRegisterDto(string Username, string Email, string Password, string Role);