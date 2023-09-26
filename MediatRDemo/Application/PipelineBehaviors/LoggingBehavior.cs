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
        _logger.LogInformation(
            "Starting request {RequestName}, {DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        if (result.IsFailure)
        {            
            _logger.LogError(
                "Request failure {RequestName}, {@error}, {DateTimeUtc}", 
                typeof(TRequest).Name, 
                result.ErrorInfo, 
                DateTime.UtcNow);
        }

        _logger.LogInformation(
            "Completed request {RequestName}, {DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        return result;
    }
}
