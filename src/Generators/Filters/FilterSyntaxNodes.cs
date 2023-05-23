namespace Generators.Filters;

/// <summary>
/// Filter syntax nodes.
/// </summary>
internal static class FilterSyntaxNodes
{
    internal static bool NodePredicate(
        SyntaxNode node,
        CancellationToken cancellationToken = default) =>
        node is AttributeSyntax attribute
        && CheckAttributeName(attribute.Name)
        && !ContainsErrors(attribute)
        && BoundModelDefinition(attribute);

    internal static PrimitiveToGenerate? TargetFactory(
        GeneratorSyntaxContext context,
        CancellationToken cancellationToken = default)
    {
        if (context.Node.Parent?.Parent is not TypeDeclarationSyntax modelsDeclarationSyntax)
        {
            return null;
        }

        return ModelExtensions.GetDeclaredSymbol(context.SemanticModel, modelsDeclarationSyntax, cancellationToken) is INamedTypeSymbol type 
               && type.GetAttributes()
                   .Any(attr =>
                       attr.AttributeClass?.Name
                           is AttributeNames.PrimitiveAttributeShortName
                           or AttributeNames.PrimitiveAttributeFullName) 
            ? new(modelsDeclarationSyntax, type, "attributeType")
            : null;
    }

    private static bool CheckAttributeName(NameSyntax attributeName) =>
        NameFactory.GetNameText(attributeName)
            is AttributeNames.PrimitiveAttributeShortName
            or AttributeNames.PrimitiveAttributeFullName;

    private static bool ContainsErrors(AttributeSyntax attributeSyntax) =>
        attributeSyntax
            .GetDiagnostics()
            .Any(d => d.Severity is DiagnosticSeverity.Error);

    private static bool BoundModelDefinition(AttributeSyntax attributeSyntax) =>
        attributeSyntax.Parent?.Parent
            is TypeDeclarationSyntax model
        && ValidateModel(model);

    private static bool ValidateModel(TypeDeclarationSyntax model)
    {
        bool isPartial = false;
        bool isStatic = false;

        foreach (SyntaxToken modifier in model.Modifiers)
        {
            if (modifier.IsKind(SyntaxKind.PartialKeyword))
            {
                isPartial = true;
            }

            if (modifier.IsKind(SyntaxKind.StaticKeyword))
            {
                isStatic = true;
            }
        }

        return isPartial && !isStatic;
    }

    private static 
}