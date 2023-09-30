using MediatRDemo.Application.Errors;
using MediatRDemo.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Extensions;

public static class ApiResponseExtensions
{
    public static IActionResult ToApiResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return result.ErrorInfo!.Code switch
        {
            ErrorCodes.NotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(result)
        };
    }

    public static IActionResult ToApiResponse<TData>(this Result<TData> result, 
        Func<Result<TData>, ObjectResult>? objectResultGenerator = default)
    {
        if (result.IsSuccess)
        {
            return objectResultGenerator?.Invoke(result) ?? new OkObjectResult(result);
        }

        return result.ErrorInfo!.Code switch
        {
            ErrorCodes.NotFound => new NotFoundResult(),
            _ => new BadRequestObjectResult(result)
        };
    }
}
