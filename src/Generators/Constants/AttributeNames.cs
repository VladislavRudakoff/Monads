namespace Generators.Constants;

/// <summary>
/// Contains attribute names.
/// </summary>
internal static class AttributeNames
{
    internal const string RootPrimitiveAttributeShortName = "RootPrimitive";
    internal const string RootPrimitiveAttributeFullName = RootPrimitiveAttributeShortName + "Attribute";
    internal const string RootPrimitiveAttributeTypeName = "Monads.Attributes." + RootPrimitiveAttributeFullName;

    internal const string PrimitiveAttributeShortName = "Primitive";
    internal const string PrimitiveAttributeFullName = PrimitiveAttributeShortName + "Attribute";
    internal const string PrimitiveAttributeTypeName = "Monads.Attributes." + PrimitiveAttributeFullName;
}