namespace Generators.Models;

public sealed record PrimitiveToGenerate(TypeDeclarationSyntax Declaration, INamedTypeSymbol Type, Type AttributeGenericType)
{
    public readonly TypeDeclarationSyntax Declaration = Declaration;
    public readonly INamedTypeSymbol Type = Type;
    public readonly Type AttributeGenericType = AttributeGenericType;
}