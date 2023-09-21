using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.Extensions;

public static class ResultExtensions
{
    public static Result<TData> ToResult<TData>(this TData data)
    => new(data);
}
