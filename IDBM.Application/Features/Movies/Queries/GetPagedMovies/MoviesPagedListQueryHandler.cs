using AutoMapper;
using Imdb.Application.Contracts.Persistence;
using Imdb.Application.Providers;
using MediatR;

namespace Imdb.Application.Features.Movies.Queries.GetPagedMovies
{
    public class MoviesPagedListQueryHandler : IRequestHandler<MoviesPagedListQuery, MoviesPagedListResponse>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ISearchObjectProvider _searchObjectProvider;
        private readonly IMapper _mapper;

        public MoviesPagedListQueryHandler(IMovieRepository movieRepository, ISearchObjectProvider searchObjectProvider, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _searchObjectProvider = searchObjectProvider;
            _mapper = mapper;
        }
        public async Task<MoviesPagedListResponse> Handle(MoviesPagedListQuery request, CancellationToken cancellationToken)
        {
            var searchObject = _searchObjectProvider.GetMovieSearchObject(request.SearchTerm, request.Page, request.PageSize);
            var (movies, totalCount) = await _movieRepository.GetListByFilter(searchObject);
           
            var response = new MoviesPagedListResponse();

            

            response.Movies = _mapper.Map<List<MovieDto>>(movies).OrderByDescending(x => x.AverageRating).ToList();
            response.Page = request.Page;
            response.PageSize = request.PageSize;
            response.HasMore = request.Page * request.PageSize < totalCount;


            return response;
        }
    }
}
