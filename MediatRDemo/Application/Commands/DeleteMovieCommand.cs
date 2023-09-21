using MediatR;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.Commands;

public record DeleteMovieCommand(int MovieId) : IRequest<Result>;
