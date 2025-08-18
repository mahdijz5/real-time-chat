using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class ChatRoom
    {
        public MongoId Id { get; private set; }

        public NonEmptyString Title { get; private set; }

        public MongoId CreatorId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public static ChatRoom MkUnsafe(string id, string title, string creatorId, DateTime createdAt)
        {
            return new ChatRoom(id, title, creatorId, createdAt);
        }

        private ChatRoom(string id, string title, string creatorId, DateTime createdAt)
        {
            Id = MongoId.MkUnsafe(id);
            Id = MongoId.MkUnsafe(creatorId);
            Title = NonEmptyString.MkUnsafe(title);
            CreatedAt = createdAt;
        }
    }
}
