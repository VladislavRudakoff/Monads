namespace Generators.Factories;

internal static class PrimitiveFactory
{
    internal static PrimitiveToGenerate? CreatePrimitive(
        SemanticModel semanticModel,
        TypeDeclarationSyntax modelsDeclarationSyntax,
        AttributeSyntax attributeSyntax,
        CancellationToken token = default)
    {
        if (semanticModel.GetSymbolInfo(attributeSyntax, token).Symbol is not IMethodSymbol attributeCtorSymbol)
        {
            return null;
        }

        if (ModelExtensions.GetDeclaredSymbol(semanticModel, modelsDeclarationSyntax, token) is not INamedTypeSymbol primitiveModelSymbol)
        {
            return null;
        }

        ImmutableArray<AttributeData> attributes = primitiveModelSymbol.GetAttributes();

        if (attributes.IsDefaultOrEmpty)
        {
            return null;
        }

        AttributeData? primitiveAttributeData = attributes.FirstOrDefault(attr =>
            attr.AttributeClass?.Name
                is Attributes.PrimitiveAttributeShortName
                or Attributes.PrimitiveAttributeFullName);

        if (primitiveAttributeData is null)
        {
            return null;
        }

        string? attributePrimitiveTypeName = GetAttributePrimitiveTypeName(primitiveAttributeData, attributeCtorSymbol.ContainingType);

        if (attributePrimitiveTypeName is null)
        {
            return null;
        }

        return new(modelsDeclarationSyntax, primitiveModelSymbol, attributePrimitiveTypeName);
    }

    private static string? GetAttributePrimitiveTypeName(
        AttributeData primitiveAttributeData,
        INamedTypeSymbol attributeSymbol)
    {
        if (!attributeSymbol.IsGenericType)
        {
            if (!attributeSymbol.Equals(primitiveAttributeData.AttributeClass, SymbolEqualityComparer.Default))
            {
                return null;
            }

            if (primitiveAttributeData.ConstructorArguments.IsDefaultOrEmpty)
            {
                return (attributeSymbol.TypeArguments[0] as INamedTypeSymbol)?.ToDisplayString();
            }

            ImmutableArray<TypedConstant> args = primitiveAttributeData.ConstructorArguments;

            if (Enumerable.Any(args, arg => arg.Kind == TypedConstantKind.Error))
            {
                return null;
            }

            if (args[0].Value is INamedTypeSymbol genericArgument)
            {
                if (expr)
                {
                    
                }

                if (IsPrimitiveType(genericArgument))
                {
                    return genericArgument.ToDisplayString();
                }

                if (genericArgument.IsGenericType && genericArgument.ContainingNamespace.Name is not nameof(System))
                {
                    
                }

                var one = genericArgument.ContainingNamespace;

                return genericArgument.IsGenericType
                    ? genericArgument.TypeArguments[0].ToDisplayString()
                    : genericArgument.ToDisplayString();
            }
        }

        return (attributeSymbol.TypeArguments[0] as INamedTypeSymbol)?.ToDisplayString();
    }

    private static bool IsPrimitiveType(INamedTypeSymbol type) => type.ContainingNamespace.Name is nameof(System);
}