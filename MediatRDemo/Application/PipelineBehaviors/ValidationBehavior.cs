using FluentValidation;
using MediatR;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var errorInfos = new List<ErrorInfo>();

        foreach (var validator in _validators)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var args = validationResult
                    .Errors
                    .Select(error => new KeyValuePair<string, object>(error.PropertyName, error.AttemptedValue))
                    .ToArray();
                var errors = validationResult.Errors.Select(error => new ApplicationError(error.ErrorMessage, args));
                var errorInfo = new ErrorInfo("ValidationError", errors);

                errorInfos.Add(errorInfo);
            }
        }

        if (errorInfos.Any())
        {
            return CreateFailureResponse(new ErrorInfo("ValidationErrors", errorInfos.SelectMany(errorInfo => errorInfo.Errors)));
        }

        return await next();
    }

    private static TResponse CreateFailureResponse(ErrorInfo errorInfo)
    {
        var responseType = typeof(TResponse);

        if (responseType.IsGenericType)
        {
            var failureConstructor = responseType.GetConstructor(new[] { typeof(ErrorInfo) });
            var resultInstance = failureConstructor!.Invoke(new object[] { errorInfo });

            return (TResponse)resultInstance;
        }

        return (TResponse)(object)errorInfo;
    }
}
