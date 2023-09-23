using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Extensions;
using MediatRDemo.Application.Interfaces;
using MediatRDemo.Application.Models;
using MediatRDemo.Domain.Entities;

namespace MediatRDemo.Application.Handlers;

public class CreateMovieHandler : IRequestHandler<CreateMovieCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

        return movie.Id.ToResult();
    }
}
