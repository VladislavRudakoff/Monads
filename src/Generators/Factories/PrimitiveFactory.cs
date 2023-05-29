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