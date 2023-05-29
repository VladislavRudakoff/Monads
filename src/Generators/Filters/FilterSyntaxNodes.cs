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

    internal static PrimitiveToGenerate? TargetFactory(GeneratorSyntaxContext context, CancellationToken token = default)
    {
        if (context.Node is not AttributeSyntax attributeSyntax)
        {
            return null;
        }

        if (attributeSyntax.Parent?.Parent is not TypeDeclarationSyntax modelsDeclarationSyntax)
        {
            return null;
        }

        if (ModelExtensions.GetDeclaredSymbol(context.SemanticModel, modelsDeclarationSyntax, token) is not INamedTypeSymbol primitiveModelSymbol)
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

        return primitiveAttributeData is not null
            ? PrimitiveFactory.CreatePrimitive(context.SemanticModel, attributeSyntax, primitiveAttributeData, token)
            : null;
    }

    private static bool CheckAttributeName(NameSyntax attributeName) =>
        ModelFactory.GetNameText(attributeName)
            is Attributes.PrimitiveAttributeShortName
            or Attributes.PrimitiveAttributeFullName;

    private static bool ContainsErrors(AttributeSyntax attributeSyntax) =>
        attributeSyntax
            .GetDiagnostics()
            .Any(d => d.Severity is DiagnosticSeverity.Error);

    private static bool BoundModelDefinition(AttributeSyntax attributeSyntax) =>
        attributeSyntax.Parent?.Parent is TypeDeclarationSyntax model
        && TypeValidator.ValidateModel(model);
}