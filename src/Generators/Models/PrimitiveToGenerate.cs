namespace Generators.Models;

public sealed record PrimitiveToGenerate(TypeDeclarationSyntax Declaration, INamedTypeSymbol Type)
{
    public readonly TypeDeclarationSyntax Declaration = Declaration;
    public readonly INamedTypeSymbol Type = Type;
    public readonly bool IsPartial = Declaration.Modifiers.Any(p => p.IsKind(SyntaxKind.PartialKeyword));
    public readonly bool IsStatic = Declaration.Modifiers.Any(p => p.IsKind(SyntaxKind.StaticKeyword));
}