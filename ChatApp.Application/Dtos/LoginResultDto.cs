namespace ChatApp.Application.Dtos;

public record LoginResultDto
{
    public string Token { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
}