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

        return attributeSyntax.Parent?.Parent is not TypeDeclarationSyntax modelsDeclarationSyntax
            ? null
            : PrimitiveFactory.CreatePrimitive(context.SemanticModel, modelsDeclarationSyntax, attributeSyntax, token);
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