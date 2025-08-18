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

        public static User MkUnsafe(string id, string username, string password, IEnumerable<string> chatRoomIds)
        {
            return new User(id, username, password, chatRoomIds);
        }

        private User(string id, string username, string password, IEnumerable<string> chatRoomIds)
        {
            Id = MongoId.MkUnsafe(id);
            Username = NonEmptyString.MkUnsafe(username);
            Password = NonEmptyString.MkUnsafe(password);
            CreatedAt = DateTime.UtcNow;
            _chatRoomIds = chatRoomIds?.Select(MongoId.MkUnsafe).ToList() ?? new List<MongoId>();
        }

        public void AddChatRoom(MongoId id)
        {
            if (!_chatRoomIds.Contains(id))
            {
                _chatRoomIds.Add(id);
            }
        }
    }
}
