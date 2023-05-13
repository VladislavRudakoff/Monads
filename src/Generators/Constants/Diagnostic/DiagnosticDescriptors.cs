namespace Generators.Constants.Diagnostic;

/// <summary>
/// Contains diagnostic descriptors. 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("MicrosoftCodeAnalysisReleaseTracking", "RS2008:Enable analyzer release tracking")]
internal static class DiagnosticDescriptors
{
    private const string DiagnosticCode = "PRIM";

    internal static readonly DiagnosticDescriptor PartialModifierIsRequired = new(
        DiagnosticCode + "1000",
        DiagnosticTitles.TestDiagnostic,
        "Model must be declared partial",
        DiagnosticCategories.Warning,
        DiagnosticSeverity.Warning,
        true);
}