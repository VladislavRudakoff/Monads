﻿namespace Monads.Attributes;

/// <summary>
/// Settings to be used by primitives source generator.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
public sealed class RootPrimitiveAttribute<T> : Attribute
{
    public readonly Type Type = typeof(T);
}