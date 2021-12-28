using Imdb.Domain.Exceptions;
using Imdb.Domain.Common;
using Imdb.Domain.ValueObjects;

namespace Imdb.Domain.Enteties
{
    public class Actor : Entity
    {
        public virtual Name Name { get; private set; }

        private readonly List<Movie> _movies = new List<Movie>();
        public virtual IReadOnlyList<Movie> Movies => _movies.ToList();

        protected Actor()
        {

        }

        public Actor(Name name)
        {
            Name = name;
        }
        public void AddMovie(Movie movie)
        {
            if (_movies.Any(x => x.Title == movie.Title && x.ReleaseDate == movie.ReleaseDate))
                throw new DomainException($"Movie {movie.Title}, released on {movie.ReleaseDate.ToShortDateString()} is already added!");

            _movies.Add(movie);
        }

    }
}
