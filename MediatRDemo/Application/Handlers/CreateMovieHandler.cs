using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Events;
using MediatRDemo.Application.Extensions;
using MediatRDemo.Application.Interfaces;
using MediatRDemo.Application.Models;
using MediatRDemo.Domain.Entities;

namespace MediatRDemo.Application.Handlers;

public class CreateMovieHandler : IRequestHandler<CreateMovieCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;

    public CreateMovieHandler(IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result<int>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Movie.Title,
            ReleaseYear = request.Movie.ReleaseYear
        };

        await _unitOfWork.Movies.AddAsync(movie, cancellationToken);
        await _unitOfWork.CompleteAsync();

        request.Movie.Id = movie.Id;
        await _publisher.Publish(new MovieCreatedEvent(request.Movie), cancellationToken);

        return movie.Id.ToResult();
    }
}
