﻿namespace Generators;

[Generator]
public sealed class PrimitiveGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext generatorContext)
    {
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }

        PrimitiveComparer comparer = PrimitiveComparer.Instance;

        IncrementalValueProvider<ImmutableArray<PrimitiveToGenerate>> pipeline = generatorContext.SyntaxProvider
            .CreateSyntaxProvider(
                FilterSyntaxNodes.NodePredicate,
                FilterSyntaxNodes.TargetFactory)
            .Where(t => t is not null)!
            .WithComparer(comparer)
            .Collect()
            .SelectMany((targets, _) => targets.Distinct(comparer))
            .Collect();

        IncrementalValueProvider<(Compilation Left, ImmutableArray<PrimitiveToGenerate> Right)> compilation =
            generatorContext.CompilationProvider.Combine(pipeline);

        generatorContext.RegisterSourceOutput(compilation, Execute);
    }

    private static void Execute(
        SourceProductionContext context,
        (Compilation Compilation, ImmutableArray<PrimitiveToGenerate> Models) valueProvider)
    {
        if (valueProvider.Models.IsDefaultOrEmpty)
        {
            return;
        }

        foreach (PrimitiveToGenerate? targetModel in valueProvider.Models)
        {
            string? typeNamespace = targetModel.Type.ContainingNamespace.IsGlobalNamespace
                ? null
                : targetModel.Type.ContainingNamespace.ToString();

            string code = GenerateCode(targetModel, typeNamespace);

            context.AddSource($"{targetModel.Type.Name}.g", code);
        }
    }

    private static string GenerateCode(PrimitiveToGenerate primitiveModel, string? typeNamespace)
    {
        bool isReferenceType =
            primitiveModel.Declaration.IsKind(SyntaxKind.ClassDeclaration)
            || primitiveModel.Declaration.IsKind(SyntaxKind.RecordDeclaration);

        string modifiers = string.Join(" ", primitiveModel.Declaration.Modifiers);

        return isReferenceType
            ? GenerateReferenceType(primitiveModel, typeNamespace, modifiers)
            : GenerateValueType(primitiveModel, typeNamespace, modifiers);
    }

    private static string GenerateValueType(
        PrimitiveToGenerate primitiveModel,
        string? typeNamespace,
        string modifiers)
    {
        bool isRecordStruct = primitiveModel.Declaration.IsKind(SyntaxKind.RecordStructDeclaration);
        bool isPublic = primitiveModel.Type.DeclaredAccessibility is Accessibility.Public;
        bool isInternal = primitiveModel.Type.DeclaredAccessibility is Accessibility.Internal;

        string keyword = isRecordStruct ? Keywords.RecordStruct : Keywords.Struct;

        string name = primitiveModel.Type.Name;

        return @$"// <auto-generated />

{(typeNamespace is null ? null : $@"namespace {typeNamespace}
{{")}
   {modifiers} {keyword} {name}
   {{
        private 

        public readonly int Value;
   }}
{(typeNamespace is null ? null : @"}
")}";
    }

    private static string GenerateReferenceType(
        PrimitiveToGenerate primitiveModel,
        string? typeNamespace,
        string modifiers)
    {
        string keyword = primitiveModel.Declaration.Keyword.Text;

        string name = primitiveModel.Type.Name;

        return @$"// <auto-generated />

{(typeNamespace is null ? null : $@"namespace {typeNamespace}
{{")}
   {modifiers} {keyword} {name}
   {{
      public readonly int Age;
   }}
{(typeNamespace is null ? null : @"}
")}";
    }
}