using MediatR;
using MediatRDemo.Application.Commands;
using MediatRDemo.Application.Dtos;
using MediatRDemo.Application.Extensions;
using MediatRDemo.Application.Models;
using MediatRDemo.Application.Queries;
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
    public async Task<IActionResult> GetMovie(int id)
    {
        var result = await _mediator.Send(new GetMovieByIdQuery(id));

        return result.ToApiResponse();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie(MovieDto movie)
    {
        var result = await _mediator.Send(new CreateMovieCommand(movie));

        if (result.IsFailure)
        {
            return result.ToApiResponse();
        }

        return CreatedAtAction(nameof(GetMovie), new { id = result.Data!.Id }, result);
    }
}
