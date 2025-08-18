
using ChatApp.Application.Commands.ChatRoom;
using FluentValidation;

namespace ChatApp.Application.Validators;

public class CreateChatRoomCommandValidator : AbstractValidator<CreateChatRoomCommand>
{
    public CreateChatRoomCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Chat room title is required.");


        RuleFor(x => x.CreatorId)
            .NotEmpty().WithMessage("CreatorId is required.");
    }
}