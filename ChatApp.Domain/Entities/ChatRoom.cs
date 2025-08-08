using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class ChatRoom
    {
        public MongoId Id { get; private set; }

        public NonEmptyString Title { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public static ChatRoom MkUnsafe(string id, string title)
        {
            return new ChatRoom(id, title);
        }

        private ChatRoom(string id, string title)
        {
            Id = MongoId.MkUnsafe(id);
            Title = NonEmptyString.MkUnsafe(title);
            CreatedAt = DateTime.UtcNow;
        }
    }
}
