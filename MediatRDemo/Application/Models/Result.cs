namespace MediatRDemo.Application.Models;

public class Result
{
    protected Result() { }

    public ErrorInfo? Error { get; init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    public static Result Success()
        => new();

    public static Result Failure(ErrorInfo error)
        => new() { Error = error };

    public static Result NotFound(object id)
        => new() { Error = new ErrorInfo("NotFound", new[] { "Element with id {id} not found." }, id) };

}

public class Result<TData> : Result
{
    private Result() { }

    public TData? Data { get; init; }

    public static Result<TData> Success(TData data)
        => new() { Data = data };

    public static new Result<TData> Failure(ErrorInfo error)
        => new() { Error = error };

    public static new Result<TData> NotFound(object id)
        => new() { Error = new ErrorInfo("NotFound", new[] { "Element with id {id} not found." }, id) };    
}