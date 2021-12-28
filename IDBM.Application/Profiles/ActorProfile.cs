using AutoMapper;
using Imdb.Application.Features.Movies.Queries.GetPagedMovies;
using Imdb.Domain.Enteties;

namespace Imdb.Application.Profiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorDto>()
                .ForMember(x => x.FirstName, y => y.MapFrom(x => x.Name.First))
                .ForMember(x => x.LastName, y => y.MapFrom(x => x.Name.Last))
                .ReverseMap();

        }
    }
}
