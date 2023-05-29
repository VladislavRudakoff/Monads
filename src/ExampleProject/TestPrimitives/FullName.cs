namespace ExampleProject.TestPrimitives;

[Primitive(typeof(PhoneNumber<int>))]
partial struct FullName
{
    public FullName()
    {
        
    }

    private readonly string Text = "Text";

    internal int Age = 12;

}