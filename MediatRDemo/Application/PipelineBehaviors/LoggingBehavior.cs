using System.Diagnostics;
using MediatR;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var startTime = Stopwatch.GetTimestamp();
        _logger.LogInformation(
            "Starting request {RequestName}", 
            typeof(TRequest).Name);

        var result = await next();

        if (result.IsFailure)
        {
            _logger.LogError(
                "Request failure {RequestName}, {@Error}",
                typeof(TRequest).Name,
                result.ErrorInfo);
        }

        var elapsedTime = Stopwatch.GetElapsedTime(startTime);
        _logger.LogInformation(
            "Completed request {RequestName}, ElapsedTime: {ElapsedTime}",
            typeof(TRequest).Name,
            elapsedTime);

        return result;
    }
}
