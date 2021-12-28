using CSharpFunctionalExtensions;

namespace Imdb.Domain.ValueObjects
{
    public class RatingValue : ValueObject
    {
        public int Value { get; }

        private RatingValue(int value)
        {
            Value = value;
        }

        public static Result<RatingValue> Create(int value)
        {
            if (value < 0 || value > 5)
                return Result.Failure<RatingValue>("Rate value is invalid");

            return Result.Success(new RatingValue(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(RatingValue rateValue)
        {
            return rateValue.Value.ToString();
        }
    }
}
