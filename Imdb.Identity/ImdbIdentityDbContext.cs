using Imdb.Identity.Enteties;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imdb.Identity
{
    public class ImdbIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ImdbIdentityDbContext(DbContextOptions<ImdbIdentityDbContext> options) : base(options)
        {
        }
    }
}
