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

        return result.Error!.Code switch
        {
            Constants.NotFoundCode => new NotFoundResult(),
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
            Constants.NotFoundCode => new NotFoundResult(),
            _ => new BadRequestObjectResult(result)
        };
    }
}
