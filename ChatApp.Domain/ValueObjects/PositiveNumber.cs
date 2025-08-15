namespace ChatApp.Domain.ValueObjects
{
    public record PositiveNumber
    {
        public int Value { get; }

        public PositiveNumber(int value)
        {
            if (!Is(value))
            {
                throw new ArgumentException("Invalid PositiveNumber.", nameof(value));
            }
            Value = value;
        }

        public static bool Is(int value) => value > 0;

        public static PositiveNumber? Mk(int value)
        {
            if (Is(value))
            {
                return new PositiveNumber(value);
            }
            return null;
        }

        public static PositiveNumber MkUnsafe(int value)
        {
            return new PositiveNumber(value);
        }

        public override string ToString() => Value.ToString();
    }
}