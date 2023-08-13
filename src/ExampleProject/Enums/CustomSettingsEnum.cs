namespace ExampleProject.Enums;

[Flags]
public enum CustomSettingsEnum
{
    Default = 0,
    CustomToString = 1 << 0,
    CustomEquals = 1 << 1,
    NullableDisable = 1 << 2
}