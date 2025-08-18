using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Infrastructure.Model;

public class ChatRoomModel : AbstractModel
{
    public string Title { get; set; } = string.Empty;

    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string CreatorId { get; set; }
}