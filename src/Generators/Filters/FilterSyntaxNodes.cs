using Generators.Constants.Diagnostic;

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
        BaseTypeDeclarationSyntax modelsDeclarationSyntax = (BaseTypeDeclarationSyntax)context.Node.Parent!.Parent!;

        return context.SemanticModel
                   .GetDeclaredSymbol(modelsDeclarationSyntax, cancellationToken) is INamedTypeSymbol type 
               && type.GetAttributes()
                   .Any(attr =>
                       attr.AttributeClass?.ToDisplayString(NullableFlowState.None, SymbolDisplayFormat.FullyQualifiedFormat) 
                           is AttributeNames.PrimitiveAttributeTypeName
                           or AttributeNames.RootPrimitiveAttributeTypeName) 
            ? new(modelsDeclarationSyntax, type)
            : null;
    }

    private static bool CheckAttributeName(NameSyntax attributeName) =>
        NameFactory.GetNameText(attributeName)
            is AttributeNames.PrimitiveAttributeShortName
            or AttributeNames.PrimitiveAttributeFullName
            or AttributeNames.RootPrimitiveAttributeShortName
            or AttributeNames.RootPrimitiveAttributeFullName;

    private static bool ContainsErrors(AttributeSyntax attributeSyntax) =>
        attributeSyntax
            .GetDiagnostics()
            .Any(d => d.Severity is DiagnosticSeverity.Error);

    private static bool BoundModelDefinition(AttributeSyntax attributeSyntax) =>
        attributeSyntax.Parent?.Parent
            is ClassDeclarationSyntax
            or StructDeclarationSyntax
            or RecordDeclarationSyntax;

    internal static bool ValidateModel(SourceProductionContext context, PrimitiveToGenerate primitiveModel)
    {
        bool isValid = true;

        foreach (SyntaxToken syntaxToken in primitiveModel.Declaration.Modifiers)
        {
            if (syntaxToken.ValueText is Modifiers.Partial)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.TestDescriptor, primitiveModel.Declaration.GetLocation()));
                isValid = false;
            }
        }

        return isValid;
    }
}