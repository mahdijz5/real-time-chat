
namespace ChatApp.Domain.ValueObjects
{
    public record NonEmptyString
    {
        public string Value { get; }

        public NonEmptyString(string value)
        {
            if (!Is(value))
            {
                throw new ArgumentException("Invalid NonEmptyString.", nameof(value));
            }
            Value = value;
        }

        public static bool Is(string value) => !String.IsNullOrWhiteSpace(value);

        public static NonEmptyString? Mk(string value)
        {
            if (Is(value))
            {
                return new NonEmptyString(value);
            }
            return null;
        }

        public static NonEmptyString MkUnsafe(string value)
        {
            return new NonEmptyString(value);
        }

        public override string ToString() => Value;
    }
}


