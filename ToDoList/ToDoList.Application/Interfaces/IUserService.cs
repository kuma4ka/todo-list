using ToDoList.Application.DTOs.Result;
using ToDoList.Application.DTOs.UserRelated;

namespace ToDoList.Application.Interfaces;

public interface IUserService
{
    public Task<Result> RegisterUserAsync(UserRegisterDto userRegisterDto);
    Task<Result<string>> LoginAsync(UserLoginDto userLoginDto);
}