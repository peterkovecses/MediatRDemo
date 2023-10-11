using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Queries;
using MediatRDemo.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MediatRDemo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MoviesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovie(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetMovieByIdQuery(id), cancellationToken);

        return result.ToApiResponse();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(MovieDto movie, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateMovieCommand(movie), cancellationToken);

        return result
            .ToApiResponse(result => CreatedAtAction(
                nameof(GetMovie), 
                new { id = result.Data! }, 
                result));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteMovieCommand(id), cancellationToken);

        return result.ToApiResponse();
    }
}
