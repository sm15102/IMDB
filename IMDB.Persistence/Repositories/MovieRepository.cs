using Imdb.Application.Contracts.Persistence;
using Imdb.Domain.Enteties;
using Imdb.Domain.SearchObjets;
using IMDB.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Imdb.Persistence.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ImdbDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<Movie> GetByIdWithRatingsAsync(Guid id)
        {
            var entity = await Entity.Include(x => x.Ratings)
                .FirstAsync(x => x.Id == id);

            return entity;
        }



        public async Task<(List<Movie>, int totalCount)> GetListByFilter(MovieSearchObject search)
        {
            var query = Entity.Include(x => x.Cast).AsQueryable();

            if (search.Fts is not null)
            {
                query = query.Where(x => EF.Functions.FreeText(x.Title, search.Fts) || EF.Functions.FreeText(x.Description, search.Fts));
            }
            if (search.ReleaseDate is not null)
            {
                query = query.Where(x => x.ReleaseDate.Date == search.ReleaseDate.Value.Date);
            }
            if (search.ReleaseDateGTE is not null)
            {
                query = query.Where(x => x.ReleaseDate.Date >= search.ReleaseDateGTE.Value.Date);
            }
            if (search.ReleaseDateLTE is not null)
            {
                query = query.Where(x => x.ReleaseDate.Date <= search.ReleaseDateLTE.Value.Date);
            }
            if (search.AverageRating is not null)
            {
                query = query.Where(x => x.AverageRating == search.AverageRating.Value);
            }
            if (search.AverageRatingGTE is not null)
            {
                query = query.Where(x => x.AverageRating >= search.AverageRatingGTE.Value);
            }
            if (search.AverageRatingLTE is not null)
            {
                query = query.Where(x => x.AverageRating <= search.AverageRatingLTE.Value);
            }

            query = query.OrderByDescending(x => x.AverageRating);

            var totalCount = query.Count();

            query = query.Skip((search.Page - 1) * search.PageSize).Take(search.PageSize);

            return (await query.AsNoTracking().ToListAsync(), totalCount);
        }






    }
}
