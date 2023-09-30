﻿using MediatRDemo.Application.Errors;
using MediatRDemo.Application.Models;
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
            await SetResponse(context);
        }
    }

    private static async Task SetResponse(HttpContext context)
    {
        var result = Result.Failure(new ErrorInfo(ErrorCodes.ServerError, new[] { new ApplicationError(ErrorMessages.ServerError) }));
        var jsonContent = JsonSerializer.Serialize(result);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(jsonContent);
    }
}
