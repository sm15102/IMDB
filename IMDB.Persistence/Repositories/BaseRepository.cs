using Imdb.Application.Contracts.Persistence;
using Imdb.Domain.Exceptions;
using IMDB.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Imdb.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly ImdbDbContext _dbContext;

        public DbSet<T> Entity { get; private set; }

        public BaseRepository(ImdbDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Entity = _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Entity.FindAsync(id);

            if(entity is null)
            {
                throw new DomainException($"Resource with id {id} not found");
            }

            return entity;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await Entity.AddAsync(entity);

            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await Entity.FindAsync(id);

            if (entity is null)
            {
                throw new DomainException($"Resource with id {id} not found");
            }

            _dbContext.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
