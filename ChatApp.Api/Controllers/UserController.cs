using ChatApp.Application.Commands.Auth;
using ChatApp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[AllowAnonymous]
public class UserController : BaseApiController
{

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register(RegisterUserCommand command)
    {
        Console.WriteLine("Called");
        await Mediator.Send(command);
        return Ok(new { Message = "Registration successful." });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResultDto>> Login(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}