using Microsoft.AspNetCore.Identity;
using ToDoList.Application.Interfaces.UserRelated;

namespace ToDoList.Application.Wrappers;

public class UserManagerWrapper<TUser>(UserManager<TUser> userManager) : IUserManager<TUser>
    where TUser : class
{
    public Task<IdentityResult> CreateAsync(TUser user, string password)
    {
        return userManager.CreateAsync(user, password);
    }

    public Task<TUser?> FindByIdAsync(string id)
    {
        return userManager.FindByIdAsync(id);
    }
    
    public Task<TUser?> FindByEmailAsync(string email)
    {
        return userManager.FindByEmailAsync(email);
    }

    public Task<bool> CheckPasswordAsync(TUser user, string password)
    {
        return userManager.CheckPasswordAsync(user, password);
    }

    public Task<IList<string>> GetRolesAsync(TUser user)
    {
        return userManager.GetRolesAsync(user);
    }

    public Task<IdentityResult> AddToRoleAsync(TUser user, string role)
    {
        return userManager.AddToRoleAsync(user, role);
    }
}