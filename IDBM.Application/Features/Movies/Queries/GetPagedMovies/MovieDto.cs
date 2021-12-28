namespace Imdb.Application.Features.Movies.Queries.GetPagedMovies
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string CoverImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal AverageRating { get; set; }
        public List<ActorDto> Cast { get; set; }
    }
}
