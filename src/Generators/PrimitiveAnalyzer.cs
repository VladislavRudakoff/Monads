namespace Generators;

//[DiagnosticAnalyzer(LanguageNames.CSharp)]
//public class PrimitiveAnalyzer : DiagnosticAnalyzer
//{
//    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
//        ImmutableArray.Create(DiagnosticDescriptors.PartialModifierIsRequired, DiagnosticDescriptors.StaticModifierIsForbidden);

//    public override void Initialize(AnalysisContext context)
//    {
//        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
//        context.EnableConcurrentExecution();

//        context.RegisterSymbolAction(AnalyzeNamedType, SymbolKind.NamedType);
//    }

//    private static void AnalyzeNamedType(SymbolAnalysisContext context)
//    {
//        if (!Debugger.IsAttached)
//        {
//            Debugger.Launch();
//        }

//        var type = (INamedTypeSymbol)context.Symbol;

//        foreach (var declaringSyntaxReference in type.DeclaringSyntaxReferences)
//        {
//            if (declaringSyntaxReference.GetSyntax()
//                is not ClassDeclarationSyntax classDeclaration)
//            {
//                continue;
//            }

//            var error = Diagnostic.Create(DiagnosticDescriptors.PartialModifierIsRequired,
//                classDeclaration.Identifier.GetLocation(),
//                type.Name);
//            context.ReportDiagnostic(error);
//        }
//    }
//}