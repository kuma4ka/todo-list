using FluentValidation;
using ToDoList.Application.DTOs.TaskRelated;

namespace ToDoList.Application.Validators.TaskRelated;

public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskDtoValidator()
    {
        RuleFor(x => x.TaskTitle)
            .NotEmpty().WithMessage("Task title is required.")
            .MinimumLength(4).WithMessage("Minimal length of a task title is 4 characters.")
            .MaximumLength(30).WithMessage("Maximal length of a task title is 30 characters.");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Task description is required.")
            .MinimumLength(4).WithMessage("Minimal length of a task description is 4 characters.")
            .MaximumLength(100).WithMessage("Maximal length of a task description is 30 characters.");
    }
}