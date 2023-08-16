//namespace Monads.Common.Abstractions;

///// <summary>
///// Abstraction for custom primitives.
///// </summary>
///// <typeparam name="T">Type of custom primitive.</typeparam>
///// <typeparam name="TU">Type that represents the primitive.</typeparam>
///// <param name="Value">Primitive value.</param>
//public abstract record Primitive<T, TU>(TU Value) where T : Primitive<T, TU>, new()
//{
//    public override string ToString() => Value?.ToString() ?? string.Empty;

//    /// <summary>
//    /// Leads from object <see cref="TU"/> to object <see cref="T"/>. 
//    /// </summary>
//    /// <param name="primitive"></param>
//    public static explicit operator Primitive<T, TU>(TU primitive) => new T { Value = primitive };

//    /// <summary>
//    /// Leads from object <see cref="T"/> to object <see cref="TU"/>.
//    /// </summary>
//    /// <param name="customPrimitive"></param>
//    public static implicit operator TU(Primitive<T, TU> customPrimitive) => customPrimitive.Value;
//}