﻿namespace Generators.Constants;

/// <summary>
/// Contains attributes and names.
/// </summary>
internal static class Attributes
{
    internal const string PrimitiveAttributeShortName = "Primitive";
    internal const string PrimitiveAttributeFullName = PrimitiveAttributeShortName + "Attribute";

    internal const string AttributesDeclaration = $@"
namespace Monads.Attributes
{{
    /// <summary>
    /// Attribute denoting custom primitives.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class {PrimitiveAttributeFullName}<T> : System.Attribute
    {{
    }}

    /// <summary>
    /// Attribute denoting custom primitives.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public sealed class {PrimitiveAttributeFullName} : System.Attribute
    {{
        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name=""primitiveType"">The type of the primitive being taken as the basis.</param>
        public PrimitiveAttribute(Type primitiveType) =>
            Type = primitiveType;

        /// <summary>
        /// The type of the primitive being taken as the basis.
        /// </summary>
        public Type Type {{ get; }}
    }}
}}";
}