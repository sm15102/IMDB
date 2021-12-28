using Imdb.Application.Features.Movies.Commands.RateMovie;
using Imdb.Application.Features.Movies.Queries.GetPagedMovies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Imdb.api.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search", Name = "GetMoviesByFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<MoviesPagedListResponse>> GetMoviesPagedList([FromQuery] MoviesPagedListQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);  
        }

        [Authorize]
        [HttpPut("{id:guid}/rate/{value:int}", Name = "RateMovie")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> RateMovie(Guid id, int value)
        {
            var command = new RateMovieCommand { MovieId = id, Value = value };
            _ = await _mediator.Send(command);
            return NoContent();
        }
    }
}
