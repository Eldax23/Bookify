namespace Bookify.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && Error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && Error == Error.None)
        {
            throw new InvalidOperationException();
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public bool IsSuccess { get;  }
    public bool IsFailure => !IsSuccess;
    public Error Error { get;  }

    public static Result Success() => new Result(true, Error.None);

    public static Result Failure(Error error) => new Result(false, error);
    
    public static Result<T> Success<T>(T value) => new Result<T>(value, true, Error.None);
    
    public static Result<T> Failure<T>(Error error) => new Result<T>(default, false, error);

    public static Result<T> Create<T>(T value) => value is not null ? Success<T>(value) : Failure<T>(Error.NullVal);

};


public class Result<T> : Result
{
    public readonly T? _value;
    protected  internal Result(T value , bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }
    
    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("cannot access value");

    // automatically convert any value give to a Result.Create() version
    // ex: Result<string> v = "val" --> Result.Create("val");
    public static implicit operator Result<T>(T value) => Create<T>(value);
}