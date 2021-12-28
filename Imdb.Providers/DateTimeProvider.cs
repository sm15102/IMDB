using Imdb.Application.Providers;

namespace Imdb.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.UtcNow;


        public DateOnly Today() => DateOnly.FromDateTime(DateTime.UtcNow);

    }
}
