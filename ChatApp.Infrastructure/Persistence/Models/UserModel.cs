
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Infrastructure.Model;

public class UserModel : AbstractModel
{

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public List<string> ChatRoomIds { get; set; }
}