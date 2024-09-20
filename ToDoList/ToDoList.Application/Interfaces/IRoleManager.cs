using Microsoft.AspNetCore.Identity;

namespace ToDoList.Application.Interfaces;

public interface IRoleManager<in TRole>
{
    Task<bool> RoleExistsAsync(string roleName);
    Task<IdentityResult> CreateAsync(TRole role);
}