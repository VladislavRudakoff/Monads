namespace Generators.Validators;

internal static class TypeValidator
{
    internal static bool ValidateModel(TypeDeclarationSyntax model)
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
}