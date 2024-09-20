using Microsoft.AspNetCore.Identity;
using ToDoList.Application.DTOs.Result;
using ToDoList.Application.DTOs.UserRelated;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Interfaces.UserRelated;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services;

public class UserService(
    IJwtService jwtService,
    IUserManager<User> userManager,
    IRoleManager<IdentityRole> roleManager
    ) : IUserService
{
    public async Task<Result> RegisterUserAsync(UserRegisterDto userRegisterDto)
    {
        var user = new User
        {
            UserName = userRegisterDto.Username,
            Email = userRegisterDto.Email
        };

        try
        {
            var result = await userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(e => e.Description).ToList());
            }

            if (!await roleManager.RoleExistsAsync(userRegisterDto.Role))
            {
                await roleManager.CreateAsync(new IdentityRole(userRegisterDto.Role));
            }

            await userManager.AddToRoleAsync(user, userRegisterDto.Role);
            
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure([ex.Message]);
        }
    }

    public async Task<Result<string>> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await userManager.FindByEmailAsync(userLoginDto.Email);

        if (user is null || await userManager.CheckPasswordAsync(user, userLoginDto.Password))
        {
            return Result<string>.Failure(["Invalid credentials"]);
        }

        var roles = await userManager.GetRolesAsync(user);
        var userDto = new UserDto(user.Id, user.UserName, user.Email, roles);
        var token = jwtService.GenerateJwtToken(userDto);
        
        return Result<string>.Success(token);
    }
}