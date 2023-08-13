namespace Monads.Interfaces;

public interface ILogicAsync<in T>
{
    public Task ValidateAsync(T primitive);
}