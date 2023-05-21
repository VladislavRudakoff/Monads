namespace Monads.Attributes;

/// <summary>
/// Attribute denoting custom primitives.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PrimitiveAttribute<T> : Attribute
{
    /// <summary>
    /// The type of the primitive being taken as the basis.
    /// </summary>
    public readonly Type Type = typeof(T);
}

/// <summary>
/// Attribute denoting custom primitives.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PrimitiveAttribute : Attribute
{
    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="primitiveType">The type of the primitive being taken as the basis.</param>
    public PrimitiveAttribute(Type primitiveType) =>
        Type = primitiveType;

    /// <summary>
    /// The type of the primitive being taken as the basis.
    /// </summary>
    public Type Type { get; }
}