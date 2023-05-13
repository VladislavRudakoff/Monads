namespace Monads.UnitTests.Common.Primitives;

/// <summary>
/// Tests for <see cref="IPrimitive{T,TU}"/>
/// </summary>
public partial class PrimitiveTests
{
    [Fact]
    public void ImplicitCast_Primitive_SimpleValue()
    {
        Guid userIdResult = UserIdPrimitive;
        int postIdResult = PostIdPrimitive;
        string firstNameResult = FirstNamePrimitive;

        Assert.Equal(UserIdValue, userIdResult);
        Assert.Equal(PostIdValue, postIdResult);
        Assert.Equal(FirstNameValue, firstNameResult);
    }

    [Fact]
    public void ExplicitCast_SimpleValue_Primitive()
    {
        UserId userIdResult = (UserId)UserIdValue;
        PostId postIdResult = (PostId)PostIdValue;
        FirstName firstNameResult = (FirstName)FirstNameValue;

        Assert.Equal(UserIdPrimitive, userIdResult);
        Assert.Equal(PostIdPrimitive, postIdResult);
        Assert.Equal(FirstNamePrimitive, firstNameResult);
    }
}