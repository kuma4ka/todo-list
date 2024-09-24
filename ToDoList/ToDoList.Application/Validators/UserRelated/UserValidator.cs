using FluentValidation;
using ToDoList.Application.Interfaces.UserRelated;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Validators.UserRelated;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IUserManager<User> userManager)
    {
        RuleFor(user => user)
            .NotNull().WithMessage("User not found.");
        
        RuleFor(user => user)
            .MustAsync(async (user, cancellation) =>
            {
                var roles = await userManager.GetRolesAsync(user);
                return roles.Contains("User");
            })
            .WithMessage("User does not have permission to perform this action.");
    }
}