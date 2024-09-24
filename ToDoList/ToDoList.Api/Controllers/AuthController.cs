using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.DTOs.UserRelated;
using ToDoList.Application.Interfaces.UserRelated;

namespace ToDoList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var result = await userService.RegisterUserAsync(userRegisterDto);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest(new { result.Errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var result = await userService.LoginAsync(userLoginDto);

        if (!result.Succeeded)
        {
            return Unauthorized(new { result.Errors });
        }

        return Ok(new { Token = result.Value });
    }
}