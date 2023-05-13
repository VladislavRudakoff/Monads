namespace Monads.Units;

/// <summary>
/// Unit primitive.
/// </summary>
public sealed record Unit : IComparable<Unit>
{
    /// <summary>
    /// ctor.
    /// </summary>
    private Unit()
    {
    }

    /// <summary>
    /// A cached, immutable instance of an default unit.
    /// </summary>
    public static readonly Unit Value = new();

    /// <inheritdoc />
    [Pure]
    public int CompareTo(Unit? _) => 0;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => 0;

    /// <inheritdoc />
    [Pure]
    public override string ToString() => "()";

    /// <inheritdoc />
    [Pure]
    public bool Equals(Unit? _) => true;

    [Pure]
    public static bool operator >(Unit lhs, Unit rhs) => false;

    [Pure]
    public static bool operator >=(Unit lhs, Unit rhs) => true;

    [Pure]
    public static bool operator <(Unit lhs, Unit rhs) => false;

    [Pure]
    public static bool operator <=(Unit lhs, Unit rhs) => true;

    [Pure]
    public static Unit operator +(Unit a, Unit b) => Value;

    [Pure]
    public static implicit operator ValueTuple(Unit _) => default;

    [Pure]
    public static implicit operator Unit(ValueTuple _) => Value;
}