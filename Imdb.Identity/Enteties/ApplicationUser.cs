using Microsoft.AspNetCore.Identity;

namespace Imdb.Identity.Enteties
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
