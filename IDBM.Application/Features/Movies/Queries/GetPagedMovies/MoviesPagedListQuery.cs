using MediatR;

namespace Imdb.Application.Features.Movies.Queries.GetPagedMovies
{
    public record MoviesPagedListQuery : IRequest<MoviesPagedListResponse>
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
