namespace Generators.Models;

public sealed record PrimitiveToGenerate(BaseTypeDeclarationSyntax Declaration, INamedTypeSymbol Type)
{
    public readonly BaseTypeDeclarationSyntax Declaration = Declaration;
    public readonly INamedTypeSymbol Type = Type;
}