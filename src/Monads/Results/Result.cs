namespace Monads.Results;

/// <summary>
/// Result primitive.
/// </summary>
public record Result
{
    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="isSuccess">Success or failed.</param>
    /// <param name="error">Error primitive object.</param>
    private protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new NotImplementedException();
            case false when error == Error.None:
                throw new NotImplementedException();
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    /// <summary>
    /// Represents the result of a successful calculation.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Represents the result of an erroneous calculation.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Represents the error.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Represents successfully result without value.
    /// </summary>
    /// <returns><see cref="Result"/>.</returns>
    public static Result Success() => new(true, Error.None);

    public static Result Fault(Error error) => new(false, error);
}

/// <summary>
/// Generic result primitive.
/// </summary>
/// <typeparam name="TValue">Type of object in result.</typeparam>
public sealed record Result<TValue> : Result
{
    /// <summary>
    /// Represents the result of the calculation.
    /// </summary>
    private readonly TValue value;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="isSuccess">Success or failed.</param>
    /// <param name="value">Result object.</param>
    /// <param name="error">Error primitive object.</param>
    private Result(bool isSuccess, TValue value, Error error) : base(isSuccess, error) => this.value = value;

    public static Result<TValue> Success(TValue value) => new(true, value, Error.None);

    public new static Result<TValue> Fault(Error error) => new(false, default!, error);

    public TValue Value =>
        IsSuccess
            ? value
            : throw new NotImplementedException();

    /// <summary>
    /// Unpacks the value of the result.
    /// </summary>
    /// <returns>If the action is successful, the result value is returned, if not, an exception is thrown with the text of the nested error.</returns>
    /// <exception cref="NotImplementedException">.</exception>
    public TValue Unwrap() => IsSuccess ? value : throw new NotImplementedException(Error.Code + " : " + Error.Message);

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}