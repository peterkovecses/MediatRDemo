using MediatRDemo.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Application.Extensions;

public static class ResultExtensions
{
    public static Result<TData> ToResult<TData>(this TData data)
        => Result<TData>.CreateSuccess(data);

    public static IActionResult ToApiResponse<TData>(this Result<TData> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Data);
        }

        return result.Error switch
        {
            "NotFound" => new NotFoundResult(),
            _ => new BadRequestObjectResult(result.Error)
        };
    }
}
