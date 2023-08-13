namespace ExampleProject.TestAttributes;

/// <summary>
/// Attribute denoting custom primitives.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PrimitiveAttribute<T> : System.Attribute
{

}

/// <summary>
/// Attribute denoting custom primitives.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class PrimitiveAttribute : System.Attribute
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