using MediatR;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.Queries;

public record GetMovieByIdQuery(int MovieId) : IRequest<Result<MovieDto>>;
