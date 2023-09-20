namespace MediatRDemo.Application.Models;

public class Result
{
    protected Result() { }

    public string? Error { get; init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    public static Result Failure(string error)
        => new() { Error = error };
}

public class Result<TData> : Result
{
    private Result() { }

    public TData? Data { get; init; }

    public static Result<TData> CreateSuccess(TData data)
        => new() { Data = data };

    public static Result<TData> CreateFailure(string error)
        => new() { Error = error };

    public static Result<TData> CreateNotFound()
        => new() { Error = "NotFound" };    
}