using FluentValidation;
using ToDoList.Application.DTOs.UserRelated;

namespace ToDoList.Application.Validators.UserRelated;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
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
    }
}