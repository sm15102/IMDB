using Imdb.Domain.Enteties;
using Imdb.Domain.SearchObjets;

namespace Imdb.Application.Contracts.Persistence
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<Movie> GetByIdWithRatingsAsync(Guid id);
        Task<(List<Movie>, int totalCount)> GetListByFilter(MovieSearchObject search);
    }
}
