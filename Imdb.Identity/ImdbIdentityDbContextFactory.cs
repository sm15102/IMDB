using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Imdb.Identity
{
    public class ImdbIdentityDbContextFactory : IDesignTimeDbContextFactory<ImdbIdentityDbContext>
    {
        public ImdbIdentityDbContext CreateDbContext(string[] args)
        {
            var connestionString = "Server=.;Database=ImdbIdentity;Trusted_Connection=True;";

            DbContextOptionsBuilder<ImdbIdentityDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlServer(connestionString);

            return new ImdbIdentityDbContext(optionsBuilder.Options);
        }
    }
}
