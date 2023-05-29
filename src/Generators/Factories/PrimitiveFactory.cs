namespace Generators.Factories;

internal static class PrimitiveFactory
{
    internal static PrimitiveToGenerate? CreatePrimitive(
        SemanticModel semanticModel,
        AttributeSyntax attributeSyntax,
        AttributeData primitiveAttributeData,
        CancellationToken token = default)
    {
        if (attributeSyntax.Parent?.Parent is not TypeDeclarationSyntax modelsDeclarationSyntax)
        {
            return null;
        }

        if (semanticModel.GetSymbolInfo(attributeSyntax, token).Symbol is not IMethodSymbol attributeCtorSymbol)
        {
            return null;
        }

        if (ModelExtensions.GetDeclaredSymbol(semanticModel, modelsDeclarationSyntax, token) is not INamedTypeSymbol primitiveModelSymbol)
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

            if (!primitiveAttributeData.ConstructorArguments.IsDefaultOrEmpty)
            {
                ImmutableArray<TypedConstant> args = primitiveAttributeData.ConstructorArguments;

                foreach (TypedConstant arg in args)
                {
                    if (arg.Kind == TypedConstantKind.Error)
                    {
                        return null;
                    }
                }

                return args[0].Value?.ToString();
            }
        }

        return (attributeSymbol.TypeArguments[0] as INamedTypeSymbol)?.ToDisplayString();
    }
}