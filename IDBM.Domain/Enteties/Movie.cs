using Imdb.Domain.Exceptions;
using Imdb.Domain.Common;
using Imdb.Domain.ValueObjects;

namespace Imdb.Domain.Enteties
{
    public class Movie : Entity
    {
        public string Title { get; private set; }
        public string CoverImageUrl { get; private set; }
        public string Description { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        private readonly List<Actor> _cast = new List<Actor>();
        public virtual IReadOnlyList<Actor> Cast => _cast.ToList();

        private readonly List<Rating> _ratings = new List<Rating>();
        public virtual IReadOnlyList<Rating> Ratings => _ratings.ToList();

        public decimal AverageRating { get; private set; }


        protected Movie()
            : base()
        {

        }

        public Movie(string title, string coverImageUrl, string description, DateTime releaseDate, decimal averageRating = 0m)
        {
            Title = title;
            CoverImageUrl = coverImageUrl;
            Description = description;
            ReleaseDate = releaseDate;
            AverageRating = averageRating;
        }

        public Movie AddCastMember(Actor actor)
        {
            if (_cast.Any(x => x.Name == actor.Name))
                throw new DomainException($"Cast member {actor.Name} is already added!");

            _cast.Add(actor);

            return this;
        }

        public void RateMovie(string userId, RatingValue value)
        {
            var rating = _ratings.SingleOrDefault(x => x.CreatedBy == userId);

            if (rating is not null)
            {
                _ratings.Remove(rating);
            }
            
            _ratings.Add(new Rating(value));

            AverageRating = _ratings.Any() ? (decimal)_ratings.Sum(x => x.Value.Value) / _ratings.Count : 0m;
        }
    }
}
