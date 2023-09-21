using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Interfaces;
using MediatRDemo.Application.Models;
using MediatRDemo.Domain.Entities;
using MediatRDemo.Extensions;

namespace MediatRDemo.Application.Handlers;

public class CreateMovieHandler : IRequestHandler<CreateMovieCommand, Result<MovieDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MovieDto>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = new Movie
        {
            Title = request.Movie.Title,
            ReleaseYear = request.Movie.ReleaseYear
        };

        await _unitOfWork.Movies.AddAsync(movie, cancellationToken);
        await _unitOfWork.CompleteAsync();
        request.Movie.Id = movie.Id;

        return request.Movie.ToResult();
    }
}
