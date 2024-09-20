using Microsoft.AspNetCore.Identity;
using ToDoList.Application.Interfaces;

namespace ToDoList.Application.Wrappers;

public class RoleManagerWrapper<TRole>(RoleManager<TRole> roleManager) : IRoleManager<TRole>
    where TRole : class
{
    public Task<bool> RoleExistsAsync(string roleName)
    {
        return roleManager.RoleExistsAsync(roleName);
    }

    public Task<IdentityResult> CreateAsync(TRole role)
    {
        return roleManager.CreateAsync(role);
    }
}