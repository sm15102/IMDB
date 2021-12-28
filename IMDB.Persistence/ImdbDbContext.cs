using Imdb.Application.Contracts.Identity;
using Imdb.Application.Providers;
using Imdb.Domain.Common;
using Imdb.Domain.Enteties;
using Imdb.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Persistence
{
    public class ImdbDbContext : DbContext
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Actor> Cast { get; set; }

        public ImdbDbContext(DbContextOptions<ImdbDbContext> options)
           : base(options)
        {
        }

        public ImdbDbContext(DbContextOptions<ImdbDbContext> options, ILoggedInUserService loggedInUserService, IDateTimeProvider dateTimeProvider)
            : base(options)
        {
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Movie>(new MovieEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<Rating>(new RatingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration<Actor>(new ActorEntityTypeConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.SetCreator(_loggedInUserService.UserId, _dateTimeProvider.Now());
                        break;
                    case EntityState.Modified:
                        entry.Entity.SetModificator(_loggedInUserService.UserId, _dateTimeProvider.Now());
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
