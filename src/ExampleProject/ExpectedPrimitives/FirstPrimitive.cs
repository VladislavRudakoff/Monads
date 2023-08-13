namespace ExampleProject.ExpectedPrimitives;

public partial class FirstPrimitive
{
    /// <inheritdoc />
    public void IsValid(int primitive)
    {
        if (primitive is < 100 and > 0)
        {
            throw new Exception("Test exception");
        }
    }
}