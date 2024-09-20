using ToDoList.Application.DTOs.UserRelated;

namespace ToDoList.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(UserDto userDto);
}