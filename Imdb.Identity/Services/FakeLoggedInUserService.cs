using Imdb.Application.Contracts.Identity;

namespace Imdb.Identity.Services
{
    public class FakeLoggedInUserService : ILoggedInUserService
    {
        public string UserId => "00000000-0000-0000-0000-000000000000";
    }
}
