using Imdb.Application.Providers;
using Imdb.Domain.SearchObjets;
using System.Text.RegularExpressions;

namespace Imdb.Providers
{
    public class SearchObjectProvider : ISearchObjectProvider
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        private static List<Regex> patterns = new List<Regex>
        {
                new Regex(@"(?<starsEqual>\d+) stars"),
                new Regex(@"at least (?<starsMinimum>\d+) stars"),
                new Regex(@"after (?<yearAfter>\d+)"),
                new Regex(@"older than (?<numberOfYearsAfterBefore>\d+) years"),
        };

        public SearchObjectProvider(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public MovieSearchObject GetMovieSearchObject(string? searchTerm, int page, int pageSize)
        {
            var searchObject = new MovieSearchObject
            {
                Page = page,
                PageSize = pageSize
            };

            if (searchTerm is null)
            {
                return searchObject;
            }

            searchTerm = searchTerm.Trim().ToLower();
          
            searchObject.Fts = searchTerm.Trim();

            foreach (var pattern in patterns)
            {
                var match = pattern.Match(searchTerm);

                if(match.Captures.Count > 0)
                {
                    if (match.Groups.ContainsKey("starsEqual"))
                    {
                        searchObject.AverageRating = decimal.Parse(match.Groups["starsEqual"].Value);
                    }
                    if (match.Groups.ContainsKey("starsMinimum"))
                    {
                        searchObject.AverageRatingLTE = decimal.Parse(match.Groups["starsMinimum"].Value);
                    }
                    if (match.Groups.ContainsKey("yearAfter"))
                    {
                        searchObject.ReleaseDateGTE = new DateTime(int.Parse(match.Groups["yearAfter"].Value), 1, 1);
                    }
                    if (match.Groups.ContainsKey("numberOfYearsAfterBefore"))
                    {
                        searchObject.ReleaseDateLTE = _dateTimeProvider.Now().AddYears(- int.Parse(match.Groups["numberOfYearsAfterBefore"].Value));
                    }
                }

                foreach (var captureValue in match.Captures.Select(x => x.Value))
                {
                    searchObject.Fts = searchObject.Fts.Replace(captureValue, "");
                }
            }

            return searchObject;
        }
    }
}
