using Imdb.Identity.Services;
using Imdb.Providers;
using IMDB.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Imdb.Persistence
{
    public class ImdbDbContextFactory : IDesignTimeDbContextFactory<ImdbDbContext>
    {
        public ImdbDbContext CreateDbContext(string[] args)
        {
            var connestionString = "Server=.;Database=Imdb;Trusted_Connection=True;";

            DbContextOptionsBuilder<ImdbDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(connestionString);

            return new ImdbDbContext(optionsBuilder.Options, new FakeLoggedInUserService(), new DateTimeProvider());
        }
    }
}
