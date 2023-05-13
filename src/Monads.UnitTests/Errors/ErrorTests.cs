namespace Monads.UnitTests.Errors;

/// <summary>
/// Tests for <see cref="Error"/>.
/// </summary>
public class ErrorTests
{
    private readonly string expectedString = "Monads.Error";
    private readonly string wrongString = "Microsoft.System.Text.Json";

    [Fact]
    public void ImplicitCast_Error_SuccessfullyCast()
    {
        string result = new Error(expectedString, string.Empty);

        Assert.Equal(expectedString, result);
    }

    [Fact]
    public void ImplicitCast_Error_WrongCast()
    {
        string result = new Error(wrongString, string.Empty);

        Assert.NotEqual(expectedString, result);
    }

    [Fact]
    public void None_Error_EmptyError()
    {
        Error result = Error.None;

        Assert.Equal(string.Empty, result.Code);
        Assert.Equal(string.Empty, result.Message);
    }
}