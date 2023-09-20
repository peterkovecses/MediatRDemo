﻿namespace MediatRDemo.Application.Models;

public class ErrorInfo
{
    private const string BaseMessage = "An error occurred while processing the request.";
    private const string BaseCode = "BaseError";   

    public string Code { get; init; }
    public string Message { get; init; }
    public object[] Args { get; }

    public ErrorInfo(string code = BaseCode, string message = BaseMessage, params object[] args)
    {
        Code = code;
        Message = message;
        Args = args;
    }
}