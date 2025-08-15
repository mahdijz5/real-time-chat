using MediatR;
using ChatApp.Domain.Entities;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Application.Commands.Auth.Handlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        User? isExists = await _userRepository.FindOneByUsername(NonEmptyString.MkUnsafe(request.Username));
        if (isExists != null)
        {
            throw new Exception("Username Already exists");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var newUser = CreateUser.MkUnsafe(request.Username, hashedPassword);


        await _userRepository.Create(newUser);

        return Unit.Value;
    }
}