using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    protected string? CurrentUserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    protected string? CurrentUsername => User.FindFirst(ClaimTypes.Name)?.Value;
}