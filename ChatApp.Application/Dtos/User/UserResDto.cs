namespace ChatApp.Application.Dtos;

public record UserResDto
{
    public string _id { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
}