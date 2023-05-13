namespace Generators;

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

        StringBuilder builder = new();
        builder.AppendLine("/*");

        foreach (PrimitiveToGenerate? targetModel in valueProvider.Models)
        {
            if (!FilterSyntaxNodes.ValidateModel(context, targetModel))
            {
                continue;
            }

            builder.AppendLine(targetModel.Declaration.Identifier.Text);
        }

        builder.AppendLine("*/");
        context.AddSource("TestPartialClasses", builder.ToString());
    }
}