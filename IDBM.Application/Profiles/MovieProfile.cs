using AutoMapper;
using Imdb.Application.Features.Movies.Queries.GetPagedMovies;
using Imdb.Domain.Enteties;

namespace Imdb.Application.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
        }
    }
}
