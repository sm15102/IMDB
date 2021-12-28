using MediatR;

namespace Imdb.Application.Features.Movies.Commands.RateMovie
{
    public record RateMovieCommand : IRequest<bool>
    {
        public Guid MovieId { get; init; }
        public int Value { get; init; }
    }
}
