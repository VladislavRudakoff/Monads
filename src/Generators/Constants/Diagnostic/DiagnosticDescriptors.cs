namespace Generators.Constants.Diagnostic;

/// <summary>
/// Contains diagnostic descriptors. 
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("MicrosoftCodeAnalysisReleaseTracking", "RS2008:Enable analyzer release tracking")]
internal static class DiagnosticDescriptors
{
    private const string DiagnosticSuffix = "PRIM";

    internal static readonly DiagnosticDescriptor PartialModifierIsRequired = new(
        DiagnosticSuffix + "1000",
        DiagnosticTitles.WrongModifiers,
        "The model must be declared with the partial modifier",
        DiagnosticCategories.Test,
        DiagnosticSeverity.Warning,
        true);

    internal static readonly DiagnosticDescriptor StaticModifierIsForbidden = new(
        DiagnosticSuffix + "1001",
        DiagnosticTitles.WrongModifiers,
        "The primitive model must not be static",
        DiagnosticCategories.Warning,
        DiagnosticSeverity.Warning,
        true);
}