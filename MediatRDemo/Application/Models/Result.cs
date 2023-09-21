namespace MediatRDemo.Application.Models;

public class Result
{
    protected Result() { }

    public ErrorInfo? Error { get; init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    public static Result Success()
        => new();

    public static Result<TData> Success<TData>(TData data)
    => new(data);

    public static Result Failure(ErrorInfo error)
        => new() { Error = error };

    public static Result<TData> Failure<TData>(ErrorInfo error)
        => new(error);

    public static Result NotFound(object id)
        => new() { Error = new ErrorInfo(Constants.NotFoundCode, new[] { Constants.NotFoundMessage }, id) };

    public static Result<TData> NotFound<TData>(object id)
        => new(new ErrorInfo(Constants.NotFoundCode, new[] { Constants.NotFoundMessage }, id));
}

public class Result<TData> : Result
{
    public TData? Data { get; init; }

    public Result(TData data)
    {
        Data = data;
    }

    public Result(ErrorInfo error)
    {
        Error = error;
    }
}