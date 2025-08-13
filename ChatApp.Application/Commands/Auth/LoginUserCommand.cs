using ChatApp.Application.Dtos;
using MediatR;

namespace ChatApp.Application.Commands.Auth;

public class LoginCommand : IRequest<LoginResultDto>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}