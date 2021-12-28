using Imdb.Application.Contracts.Identity;
using Imdb.Application.Contracts.Persistence;
using Imdb.Domain.Exceptions;
using Imdb.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imdb.Application.Features.Movies.Commands.RateMovie
{
    public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand, bool>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILoggedInUserService _loggedInUserService;

        public RateMovieCommandHandler(IMovieRepository movieRepository, ILoggedInUserService loggedInUserService)
        {
            _movieRepository = movieRepository;
            _loggedInUserService = loggedInUserService;
        }
        public async Task<bool> Handle(RateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdWithRatingsAsync(request.MovieId);
            var userId = _loggedInUserService.UserId;

            if (movie is null)
            {
                throw new DomainException($"Movie with id: {request.MovieId} dont exist");
            }

            var ratingValue = RatingValue.Create(request.Value);

            if(ratingValue.IsFailure)
            {
                throw new DomainException("Rating value is invalid");
            }

            movie.RateMovie(userId, ratingValue.Value);
            await _movieRepository.SaveChangesAsync();

            return true;
        }
    }
}
