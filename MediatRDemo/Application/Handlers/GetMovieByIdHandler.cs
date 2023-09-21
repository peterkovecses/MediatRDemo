using MediatR;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Interfaces;
using MediatRDemo.Application.Models;
using MediatRDemo.Application.Queries;
using MediatRDemo.Extensions;

namespace MediatRDemo.Application.Handlers;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, Result<MovieDto>>
{
    private readonly IMovieRepository _movies;

    public GetMovieByIdHandler(IUnitOfWork unitOfWork)
    {
        _movies = unitOfWork.Movies;
    }

    public async Task<Result<MovieDto>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movies.FindByIdAsync(request.MovieId, cancellationToken);
        
        if (movie is null)
        {
            return Result<MovieDto>.NotFound(request.MovieId);
        }

        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            ReleaseYear = movie.ReleaseYear,
        }.ToResult();
    }
}
