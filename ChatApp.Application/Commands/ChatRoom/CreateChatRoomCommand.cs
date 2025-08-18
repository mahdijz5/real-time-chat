using MediatR;

namespace ChatApp.Application.Commands.ChatRoom;

public class CreateChatRoomCommand : IRequest<Unit>
{
    public string Title { get; set; }
    public string CreatorId { get; set; }
}