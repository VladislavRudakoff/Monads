namespace Generators.Constants;

internal static class Interfaces
{
    internal const string LogicInterfaceDeclaration = 
        @"namespace Monads.Interfaces
{
    public interface ILogic<in T>
    {
        public void Validate(T primitive);
    }
}";

    internal static string GetLogicDeclaration(string primitiveName) => $"ILogic<{primitiveName}>";
}