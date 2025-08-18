using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class CreateChatRoom
    {
        public NonEmptyString Title { get; private set; }

        public MongoId CreatorId { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public static CreateChatRoom MkUnsafe(string title, string creatorId)
        {
            return new CreateChatRoom(title, creatorId);
        }

        private CreateChatRoom(string title, string creatorId)
        {
            Title = NonEmptyString.MkUnsafe(title);
            CreatedAt = DateTime.UtcNow;
            CreatorId = MongoId.MkUnsafe(creatorId);
        }
    }
}
