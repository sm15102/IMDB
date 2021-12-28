using Imdb.Domain.Common;
using Imdb.Domain.ValueObjects;

namespace Imdb.Domain.Enteties
{
    public class Rating : Entity
    {
        public RatingValue Value { get; private set; }

        protected Rating()
            : base()
        {

        }

        public Rating(RatingValue value)
            : base()
        {
            Value = value;
        }
    }
}
