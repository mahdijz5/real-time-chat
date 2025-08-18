namespace ChatApp.Application.Dtos;

public record ChatRoomResDto
{
    public string _id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string CreatedAt { get; init; }
}