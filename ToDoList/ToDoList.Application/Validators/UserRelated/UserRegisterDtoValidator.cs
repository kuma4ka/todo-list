using FluentValidation;
using ToDoList.Application.DTOs.UserRelated;
using ToDoList.Domain.Constants;

namespace ToDoList.Application.Validators.UserRelated;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z0-9\W]+$")
            .WithMessage("Password must only contain English letters, numbers, or symbols.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one capital letter.")
            .Matches(@"[\W]").WithMessage("Password must contain at least one symbol.")
            .Matches(@"^[a-zA-Z0-9\W]+$")
            .WithMessage("Password must only contain English letters, numbers, or symbols.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .Must(UserRolesConstants.IsRoleAllowed).WithMessage("Invalid role specified.");
    }
}