using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Interfaces;
using MediatRDemo.Application.Models;

namespace MediatRDemo.Application.Handlers;

public class DeleteMovieHandler : IRequestHandler<DeleteMovieCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovieHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movieToDelete = await _unitOfWork.Movies.FindByIdAsync(request.MovieId, cancellationToken);

        if (movieToDelete is null)
        {
            return Result.NotFound(request.MovieId);
        }

        _unitOfWork.Movies.Remove(movieToDelete);
        await _unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
