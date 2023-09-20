using FluentValidation;
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

            var errormessage = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var errorInfo = new ErrorInfo("ValidationError", errormessage);

            return CreateFailureResponse(errorInfo);
        }

        return await next();
    }

    private static TResponse CreateFailureResponse(ErrorInfo errorInfo)
    {
        var responseType = typeof(TResponse);

        if (responseType.IsGenericType &&
            responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = responseType.GetGenericArguments()[0];
            var failureMethod =
                typeof(Result<>).MakeGenericType(resultType).GetMethod("CreateFailure");

            if (failureMethod != null)
            {
                return (TResponse)failureMethod.Invoke(null, new object[] { errorInfo });
            }
        }
        else if (responseType == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(errorInfo);
        }

        throw new InvalidOperationException(
            "The TResponse type must be Result or Result<TData>.");
    }
}
