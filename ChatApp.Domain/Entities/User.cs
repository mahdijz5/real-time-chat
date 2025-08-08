using MongoDB.Bson;
using ChatApp.Domain.ValueObjects;

namespace ChatApp.Domain.Entities
{
    public class User
    {
        public MongoId Id { get; private set; }

        public NonEmptyString Username { get; private set; }

        public NonEmptyString Password { get; private set; }

        private List<MongoId> _chatRoomIds = new List<MongoId>();
        public IReadOnlyList<MongoId> ChatRoomIds => _chatRoomIds.AsReadOnly();

        public DateTime CreatedAt { get; private set; }

        public static User MkUnsafe(string id, string username, string password)
        {
            return new User(id, username, password);
        }

        private User(string id, string username, string password)
        {
            Id = MongoId.MkUnsafe(id);
            Username = NonEmptyString.MkUnsafe(username);
            Password = Username = NonEmptyString.MkUnsafe(password);
            CreatedAt = DateTime.UtcNow;
        }

        public void AddChatRoom(string id)
        {
            MongoId chatRoomId = MongoId.MkUnsafe(id);
            if (!_chatRoomIds.Contains(chatRoomId))
            {
                _chatRoomIds.Add(chatRoomId);
            }
        }
    }
}
