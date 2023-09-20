using MediatR;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.Commands;

public record CreateMovieCommand(MovieDto Movie) : IRequest<Result<MovieDto>>;
