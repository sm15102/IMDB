namespace Imdb.Application.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now();
        DateOnly Today();
    }
}
