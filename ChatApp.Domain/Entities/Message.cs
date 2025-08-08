using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public MongoId Id { get; private set; }

        public NonEmptyString Content { get; private set; }

        public MongoId SenderId { get; private set; }

        public MongoId RoomId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public static Message MkUnsafe(string id, string content, string senderId, string roomId)
        {
            return new Message(id, content, senderId, roomId);
        }

        private Message(string id, string content, string senderId, string roomId)
        {
            Id = MongoId.MkUnsafe(id);
            Content = NonEmptyString.MkUnsafe(content);
            SenderId = MongoId.MkUnsafe(senderId);
            RoomId = MongoId.MkUnsafe(roomId);
            CreatedAt = DateTime.UtcNow;
        }
    }
}
