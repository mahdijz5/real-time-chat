using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApp.Infrastructure.Model;

abstract public class AbstractModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}