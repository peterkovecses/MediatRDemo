namespace MediatRDemo.Application.Models;

public class Result
{
    protected Result() { }

    public ErrorInfo? Error { get; init; }
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;

    public static Result Failure(ErrorInfo error)
        => new() { Error = error };

}

public class Result<TData> : Result
{
    private Result() { }

    public TData? Data { get; init; }

    public static Result<TData> CreateSuccess(TData data)
        => new() { Data = data };

    public static Result<TData> CreateFailure(ErrorInfo error)
        => new() { Error = error };

    public static Result<TData> CreateNotFound(object id)
        => new() { Error = new ErrorInfo("NotFound", new[] { "Element with id {id} not found." }, id) };    
}