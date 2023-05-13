namespace Monads.UnitTests.Units;

/// <summary>
/// Tests for <see cref="Monads.Units.Unit"/>
/// </summary>
public class UnitTests
{
    private readonly Unit unit = Unit.Value;
    private readonly ValueTuple valueTuple = new();
    private const string UnitString = "()";

    [Fact]
    public void Equal_Unit_True() => Assert.Equal(unit, Unit.Value);

    [Fact]
    public void ToString_Unit_True() => Assert.Equal(UnitString, Unit.Value.ToString());

    [Fact]
    public void CompareTo_Unit_True() => Assert.Equal(0, Unit.Value.CompareTo(unit));

    [Fact]
    public void ImplicitCast_Unit_ValueTuple()
    {
        Unit result = valueTuple;

        Assert.Equal(unit, result);
    }

    [Fact]
    public void ImplicitCast_ValueTuple_Unit()
    {
        ValueTuple result = unit;

        Assert.Equal(valueTuple, result);
    }

    [Fact]
    public void Plus_Unit_DefaultValue()
    {
        Unit result = Unit.Value + Unit.Value;

        Assert.Equal(unit, result);
    }

    [Fact]
    public void EqualOperators_Unit_ExpectedResult()
    {
        bool notEqual = unit != Unit.Value;
        bool more = unit > Unit.Value;
        bool less = unit < Unit.Value;
        bool equal = unit == Unit.Value;
        bool greaterThanEqualTo = unit >= Unit.Value;
        bool lessIsEqualTo = unit <= Unit.Value;

        Assert.False(notEqual);
        Assert.False(more);
        Assert.False(less);
        Assert.True(equal);
        Assert.True(greaterThanEqualTo);
        Assert.True(lessIsEqualTo);
    }
}