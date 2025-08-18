using MediatR;
using ChatApp.Application.Dtos;
using ChatApp.Application.Interfaces;
using ChatApp.Domain.ValueObjects;
using ChatApp.Domain.Entities;
using ChatApp.Application.Commands.Auth;

namespace ChatApp.Application.Commands.ChatRoom.Handlers;

public class CreateChatRoomCommandHandler : IRequestHandler<CreateChatRoomCommand, Unit>
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IUserRepository _userRepository;

    public CreateChatRoomCommandHandler(IChatRoomRepository chatRoomRepository, IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _chatRoomRepository = chatRoomRepository;
    }

    public async Task<Unit> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.FindOneById(MongoId.MkUnsafe(request.CreatorId));
        if (user is null)
        {
            throw new UnauthorizedAccessException("User is not found.");
        }
        MongoId chatRoomId = await _chatRoomRepository.Create(CreateChatRoom.MkUnsafe(request.Title, request.CreatorId));
        user.AddChatRoom(chatRoomId);
        await _userRepository.FindOneAndUpdate(user);

        return Unit.Value;
    }
}