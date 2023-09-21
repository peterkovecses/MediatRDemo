using MediatRDemo.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Extensions;

public static class ResultExtensions
{
    public static Result<TData> ToResult<TData>(this TData data)
        => Result<TData>.Success(data);

    public static IActionResult ToApiResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return result.Error!.Code switch
        {
            "NotFound" => new NotFoundResult(),
            _ => new BadRequestObjectResult(result)
        };
    }

    public static IActionResult ToApiResponse<TData>(this Result<TData> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return result.Error!.Code switch
        {
            "NotFound" => new NotFoundResult(),
            _ => new BadRequestObjectResult(result)
        };
    }
}
