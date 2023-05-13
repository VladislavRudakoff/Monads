namespace Generators.Comparers;

internal sealed class PrimitiveComparer : IEqualityComparer<PrimitiveToGenerate>
{
    private PrimitiveComparer()
    {
    }

    public static PrimitiveComparer Instance { get; } = new();

    public bool Equals(PrimitiveToGenerate x, PrimitiveToGenerate y)
        => x.Type.Equals(y.Type, SymbolEqualityComparer.IncludeNullability);

    public int GetHashCode(PrimitiveToGenerate obj)
        => SymbolEqualityComparer.IncludeNullability.GetHashCode(obj.Type);
}