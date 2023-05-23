namespace Generators.Factories;

internal class PrimitiveFactory
{
    internal PrimitiveToGenerate Create(
        TypeDeclarationSyntax modelsDeclarationSyntax,
        INamedTypeSymbol type)
    {
        Type attributeGenericType = typeof(object);

        foreach (AttributeData attributeData in type.GetAttributes())
        {
            foreach (KeyValuePair<string, TypedConstant> argument in attributeData.NamedArguments)
            {
                if (argument is { Key: "AttributeGenericType", Value.Value: Type value })
                {
                    attributeGenericType = value;
                }
            }
        }

        //TODO: Надо придумать нормальное решение. Сейчас это шлак.
        return new PrimitiveToGenerate(modelsDeclarationSyntax, type, attributeGenericType);
    }
}