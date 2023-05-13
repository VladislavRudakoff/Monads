namespace Monads.Errors;

/// <summary>
/// Error primitive.
/// </summary>
/// <param name="Code">The error code.</param>
/// <param name="Message">The error message.</param>
public sealed record Error(string Code, string Message)
{
    public static implicit operator string(Error error) => error.Code;

    /// <summary>
    /// Gets the empty error instance.
    /// </summary>
    internal static Error None => new(string.Empty, string.Empty);
}