using MediatRDemo.Application.Errors;
using MediatRDemo.Application.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Text.Json;

namespace MediatRDemo.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occured");
            var (code, message) = GetResponseData(ex);
            await SetResponse(context, code, message);
        }
    }

    private static (HttpStatusCode, Result) GetResponseData(Exception exception)
    => exception switch
    {
        OperationCanceledException => (HttpStatusCode.Accepted, Result.Failure(new ErrorInfo(ErrorCodes.Canceled, new[] { new ApplicationError(ErrorMessages.Canceled) }))),
        _ => (HttpStatusCode.InternalServerError, Result.Failure(new ErrorInfo(ErrorCodes.ServerError, new[] { new ApplicationError(ErrorMessages.ServerError) })))
    };

    private static async Task SetResponse(HttpContext context, HttpStatusCode code, Result result)
    {
        var jsonContent = JsonSerializer.Serialize(result);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(jsonContent);
    }
}
