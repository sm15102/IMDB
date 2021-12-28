using Imdb.Application.Common;

namespace Imdb.Application.Features.Movies.Queries.GetPagedMovies
{
    public class MoviesPagedListResponse : PagedResult
    {
        public List<MovieDto> Movies { get; set; }
    }
}
