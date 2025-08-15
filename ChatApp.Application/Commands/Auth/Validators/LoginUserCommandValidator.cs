using FluentValidation;
using ChatApp.Application.Commands.Auth;

namespace ChatApp.Application.Validators;

public class LoginUserCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}