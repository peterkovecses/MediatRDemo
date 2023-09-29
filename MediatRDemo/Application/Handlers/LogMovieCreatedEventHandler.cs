using MediatR;
using MediatRDemo.Application.Events;

namespace MediatRDemo.Application.Handlers;

public class LogMovieCreatedEventHandler : INotificationHandler<MovieCreatedEvent>
{
    private readonly ILogger<LogMovieCreatedEventHandler> _logger;

    public LogMovieCreatedEventHandler(ILogger<LogMovieCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MovieCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Movie created: {@Movie}", notification.MovieDto);

        return Task.CompletedTask;
    }
}
