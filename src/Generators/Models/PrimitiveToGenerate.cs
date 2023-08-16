namespace Generators.Models;

public sealed record PrimitiveToGenerate(
    TypeDeclarationSyntax Declaration,
    INamedTypeSymbol Type,
    string AttributePrimitiveName)
{
    public readonly SyntaxKind ModelType = Declaration.Kind();
}