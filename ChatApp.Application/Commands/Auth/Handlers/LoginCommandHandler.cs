using MediatR;
using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.ValueObjects;
using ChatApp.Domain.Entities;
using System.Text.Json;
using Microsoft.Extensions.Configuration.Json;

namespace ChatApp.Application.Commands.Auth.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.FindOneByUsername(NonEmptyString.MkUnsafe(request.Username));

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password.Value))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username.Value);

        return new LoginResultDto { Token = token, Username = user.Username.ToString() };
    }
}