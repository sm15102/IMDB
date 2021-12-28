namespace Imdb.Domain.SearchObjets
{
    public class MovieSearchObject : BaseSearchObject
    {
        public string Fts { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ReleaseDateGTE { get; set; }
        public DateTime? ReleaseDateLTE { get; set; }
        public decimal? AverageRating { get; set; }
        public decimal? AverageRatingGTE { get; set; }
        public decimal? AverageRatingLTE { get; set; }
    }
}
