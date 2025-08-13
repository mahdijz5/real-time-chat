using MediatR;

namespace ChatApp.Application.Commands.Auth;

public class RegisterUserCommand : IRequest<Unit>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}