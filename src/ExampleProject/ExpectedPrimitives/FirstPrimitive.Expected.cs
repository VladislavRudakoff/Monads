namespace ExampleProject.ExpectedPrimitives;

/// <summary>
/// Custom primitive.
/// </summary>
public partial class FirstPrimitive : IEquatable<FirstPrimitive>
{
    /// <summary>
    /// Primitive value.
    /// </summary>
    public int Value { get; }

    private static readonly Func<int, bool> validationAction = value => value is > 100 or < 0;

    private static readonly Func<int, Task<bool>> validationActionAsync = async value =>
    {
        await Task.CompletedTask;

        return value is > 100 or < 0;
    };

    /// <summary>
    /// Private ctor.
    /// </summary>
    /// <param name="value">Built-in language primitive value.</param>
    private FirstPrimitive(int value) => Value = value;

    /// <summary>
    /// Leads from object <see cref="int"/> to object <see cref="FirstPrimitive"/>. 
    /// </summary>
    /// <param name="primitive">Built-in language primitive value.</param>
    public static explicit operator FirstPrimitive(int primitive) => new(primitive);

    /// <summary>
    /// Leads from object <see cref="FirstPrimitive"/> to object <see cref="int"/>.
    /// </summary>
    /// <param name="customPrimitive">Custom primitive value.</param>
    public static implicit operator int(FirstPrimitive customPrimitive) => customPrimitive.Value;

    public static bool operator ==(FirstPrimitive lhs, FirstPrimitive rhs) => lhs.Equals(rhs);

    public static bool operator !=(FirstPrimitive lhs, FirstPrimitive rhs) => !(lhs == rhs);

    /// <inheritdoc />
    public override string ToString() => Value.ToString();

    /// <summary>
    /// Create new instance.
    /// </summary>
    /// <param name="primitive">Built-in language primitive value.</param>
    /// <returns>Custom primitive.</returns>
    public static FirstPrimitive New(int primitive)
    {
        if (validationAction(primitive))
        {

        }

        return new FirstPrimitive(primitive);
    }

    /// <summary>
    /// Create new instance.
    /// </summary>
    /// <param name="primitive">Built-in language primitive value.</param>
    /// <returns>Custom primitive.</returns>
    public static FirstPrimitive? NewNullable(int primitive) =>
        !validationAction(primitive) ? null : new FirstPrimitive(primitive);

    /// <summary>
    /// Create new instance.
    /// </summary>
    /// <param name="primitive">Built-in language primitive value.</param>
    /// <returns>Custom primitive.</returns>
    public static async Task<FirstPrimitive?> NewNullableAsync(int primitive) =>
        !(await validationActionAsync(primitive)) ? null : new FirstPrimitive(primitive);

    /// <inheritdoc />
    public bool Equals(FirstPrimitive? other) => Value == other?.Value;

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType()
               && Equals((FirstPrimitive)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();
}
