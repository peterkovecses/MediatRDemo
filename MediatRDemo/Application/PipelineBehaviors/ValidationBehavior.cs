﻿using FluentValidation;
using MediatR;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>        
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = default)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is not null)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var args = validationResult
                    .Errors
                    .Select(error => new KeyValuePair<string, object>(error.PropertyName, error.AttemptedValue))
                    .ToArray();
                var errors = validationResult.Errors.Select(error => new ApplicationError(error.ErrorMessage, args));
                var errorInfo = new ErrorInfo("ValidationError", errors);

                return CreateFailureResponse(errorInfo);
            }
        }

        return await next();
    }

    private static TResponse CreateFailureResponse(ErrorInfo errorInfo)
    {
        var responseType = typeof(TResponse);

        if (responseType.IsGenericType)
        {
            var failureConstructor = responseType.GetConstructor(new[] { typeof(ErrorInfo) });
            var resultInstance = failureConstructor.Invoke(new object[] { errorInfo });
            
            return (TResponse)resultInstance;
        }

        return (TResponse)(object)errorInfo;
    }
}
