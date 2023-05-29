namespace Generators.Models;

public sealed record PrimitiveToGenerate(
    TypeDeclarationSyntax Declaration,
    INamedTypeSymbol Type,
    string AttributePrimitiveName)
{
    public readonly ModelType ModelType = Declaration.Kind() switch
    {
        SyntaxKind.RecordStructDeclaration => ModelType.RecordStruct,
        SyntaxKind.StructDeclaration => ModelType.Struct,
        SyntaxKind.ClassDeclaration => ModelType.Class,
        SyntaxKind.RecordDeclaration => ModelType.Record,
        _ => ModelType.Unknown
    };
}