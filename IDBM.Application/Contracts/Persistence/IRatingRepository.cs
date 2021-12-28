using Imdb.Domain.Enteties;

namespace Imdb.Application.Contracts.Persistence
{
    public interface IRatingRepository : IAsyncRepository<Rating>
    {
    }
}
