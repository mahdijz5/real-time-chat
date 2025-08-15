namespace ChatApp.Domain.ValueObjects
{
    public record NonNegativeNumber
    {
        public long Value { get; }

        public NonNegativeNumber(long value)
        {
            if (!Is(value))
            {
                throw new ArgumentException("Invalid NonNegativeNumber.", nameof(value));
            }
            Value = value;
        }

        public static bool Is(long value) => value >= 0;

        public static NonNegativeNumber? Mk(long value)
        {
            if (Is(value))
            {
                return new NonNegativeNumber(value);
            }
            return null;
        }

        public static NonNegativeNumber MkUnsafe(long value)
        {
            return new NonNegativeNumber(value);
        }

        public override string ToString() => Value.ToString();
    }
}