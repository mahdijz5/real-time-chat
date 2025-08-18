using MongoDB.Bson;

namespace ChatApp.Domain.ValueObjects
{
    public record MongoId
    {
        public string Value { get; }

        public MongoId(string value)
        {
            if (!Is(value))
            {
                throw new ArgumentException("Invalid MongoDB ObjectId format.", nameof(value));
            }
            Value = value;
        }

        public static bool Is(string value) => ObjectId.TryParse(value, out _);

        public static MongoId? Mk(string value)
        {
            if (Is(value))
            {
                return new MongoId(value);
            }
            return null;
        }

        public static MongoId MkUnsafe(string value)
        {
            return new MongoId(value);
        }

        public override string ToString() => Value;


    }
}


