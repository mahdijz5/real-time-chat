using ChatApp.Application.Commands.Auth;
using ChatApp.Application.Commands.ChatRoom;
using ChatApp.Application.Dtos;
using ChatApp.Application.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[Authorize]
public class ChatRoomController : BaseApiController
{

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create([FromBody] ChatRoomDto request)
    {
        var command = new CreateChatRoomCommand
        {
            Title = request.Title,
            CreatorId = CurrentUserId!
        };
        await Mediator.Send(command);
        return Ok(new { Message = "ChatRoom Creation was successful." });
    }

}