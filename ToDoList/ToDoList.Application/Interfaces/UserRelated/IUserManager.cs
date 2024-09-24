using Microsoft.AspNetCore.Identity;

namespace ToDoList.Application.Interfaces.UserRelated;

public interface IUserManager<TUser>
{
    Task<IdentityResult> CreateAsync(TUser user, string password);
    Task<TUser?> FindByIdAsync(string id);
    Task<TUser?> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(TUser user, string password);
    Task<IList<string>> GetRolesAsync(TUser user);
    Task<IdentityResult> AddToRoleAsync(TUser user, string role);
}