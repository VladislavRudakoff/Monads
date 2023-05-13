namespace Monads.UnitTests.Common.Primitives;

/// <summary>
/// Values for <see cref="PrimitiveTests"/>.
/// </summary>
public partial class PrimitiveTests
{
    public static readonly Guid UserIdValue = Guid.NewGuid();
    public static readonly int PostIdValue = 255;
    public static readonly string FirstNameValue = "John";

    public static readonly UserId UserIdPrimitive = new(UserIdValue);
    public static readonly PostId PostIdPrimitive = new(PostIdValue);
    public static readonly FirstName FirstNamePrimitive = new(FirstNameValue);

    public record UserId(Guid Value) : IPrimitive<Guid>
    {
        public static explicit operator UserId(Guid primitive) => new(primitive);

        public static implicit operator Guid(UserId customPrimitive) => customPrimitive.Value;
    }

    public record PostId(int Value) : IPrimitive<int>
    {
        public static explicit operator PostId(int primitive) => new(primitive);

        public static implicit operator int(PostId customPrimitive) => customPrimitive.Value;
    }

    public record FirstName(string Value) : IPrimitive<string>
    {
        public static explicit operator FirstName(string primitive) => new(primitive);

        public static implicit operator string(FirstName customPrimitive) => customPrimitive.Value;
    }
}