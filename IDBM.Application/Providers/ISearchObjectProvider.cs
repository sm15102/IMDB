using Imdb.Domain.SearchObjets;

namespace Imdb.Application.Providers
{
    public interface ISearchObjectProvider
    {
        MovieSearchObject GetMovieSearchObject(string? searchTerm, int page = 1, int pageSize = 10);
    }
}
