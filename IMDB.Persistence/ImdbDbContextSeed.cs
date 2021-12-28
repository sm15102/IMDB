using Imdb.Domain.Enteties;
using Imdb.Domain.ValueObjects;
using IMDB.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace Imdb.Persistence
{
    public class ImdbDbContextSeed
    {
        public async Task SeedAsync(ImdbDbContext context, ILogger<ImdbDbContext> logger)
        {
            var policy = CreatePolicy(logger, nameof(ImdbDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.Movies.Any())
                {
                    await context.AddRangeAsync(GetPreconfiguredMovies());
                    await context.SaveChangesAsync();
                }
            });
        }

        private static IEnumerable<Movie> GetPreconfiguredMovies()
        {
            return new List<Movie>()
            {
                new Movie(
                    "The Shawshank Redemption",
                    "TheShawshankRedemption.jpeg",
                    "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    new DateTime(1994, 9, 10))
                .AddCastMember(new Actor(Name.Create("Tim", "Robbins").Value))
                .AddCastMember(new Actor(Name.Create("Morgan","Freeman").Value)),
                
                new Movie(
                    "The Godfather",
                    "TheGodfather.jpeg",
                    "The Godfather follows Vito Corleone, Don of the Corleone family, as he passes the mantel to his unwilling son, Michael.",
                    new DateTime(1972, 3, 14))
                .AddCastMember(new Actor(Name.Create("Marlon", "Brando").Value))
                .AddCastMember(new Actor(Name.Create("Al","Pacino").Value)),

                new Movie(
                    "The Dark Knight",
                    "TheDarkKnight.jpeg",
                    "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    new DateTime(2008, 7, 14))
                .AddCastMember(new Actor(Name.Create("Chrstian", "Bale").Value))
                .AddCastMember(new Actor(Name.Create("Heath", "Ledger").Value)),


                new Movie(
                    "12 Angry Men",
                    "12AngryMen.jpeg",
                    "The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.",
                    new DateTime(1957, 4, 10))
                .AddCastMember(new Actor(Name.Create("Henry", "Fonda").Value))
                .AddCastMember(new Actor(Name.Create("Lee", "Cobb").Value)),

                 new Movie(
                    "Schindler's List",
                    "SchindlersList.jpeg",
                    "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.",
                    new DateTime(1993, 11, 30))
                 .AddCastMember(new Actor(Name.Create("Liam", "Neeson").Value))
                 .AddCastMember(new Actor(Name.Create("Ralph", "Fiennes").Value)),


                  new Movie(
                    "The Lord of the Rings: The Return of the King",
                    "TheLordoftheRings.jpeg",
                    "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    new DateTime(2003, 12, 1))
                  .AddCastMember(new Actor(Name.Create("Elijah", "Wood").Value))
                  .AddCastMember(new Actor(Name.Create("Viggo", "Mortensen").Value)),

                  new Movie(
                    "Pulp Fiction",
                    "PulpFiction.jpeg",
                    "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    new DateTime(1994, 5, 21))
                  .AddCastMember(new Actor(Name.Create("John", "Travolta").Value))
                  .AddCastMember(new Actor(Name.Create("Uma", "Thurman").Value)),

                  new Movie(
                    "Spider-Man: No Way Home",
                    "SpiderMan.jpeg",
                    "With Spider-Man's identity now revealed, Peter asks Doctor Strange for help. When a spell goes wrong, dangerous foes from other worlds start to appear, forcing Peter to discover what it truly means to be Spider-Man.",
                    new DateTime(2021, 12, 13))
                  .AddCastMember(new Actor(Name.Create("Tom", "Holland").Value))
                  .AddCastMember(new Actor(Name.Create("Benedict", "Cumberbatch").Value)),


                   new Movie(
                    "Fight Club",
                    "FightClub.jpeg",
                    "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",
                    new DateTime(1999, 9, 10))
                   .AddCastMember(new Actor(Name.Create("Brat", "Pitt").Value))
                   .AddCastMember(new Actor(Name.Create("Edward", "Norton").Value)),

                   new Movie(
                    "Forrest Gump",
                    "ForrestGump.jpeg",
                    "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                    new DateTime(1994, 6, 23))
                    .AddCastMember(new Actor(Name.Create("Tom", "Hanks").Value))
                    .AddCastMember(new Actor(Name.Create("Robin", "Wright").Value)),

                   new Movie(
                    "Inception",
                    "Inception.jpeg",
                    "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                    new DateTime(2010, 7, 8))
                    .AddCastMember(new Actor(Name.Create("Leaonardo", "DiCaprio").Value))
                    .AddCastMember(new Actor(Name.Create("Elliot", "Page").Value)),

                   new Movie(
                    "The Matrix",
                    "TheMatrix.jpeg",
                    "When a beautiful stranger leads computer hacker Neo to a forbidding underworld, he discovers the shocking truth--the life he knows is the elaborate deception of an evil cyber-intelligence.",
                    new DateTime(1999, 3, 24))
                   .AddCastMember(new Actor(Name.Create("Keanu", "Reeves").Value))
                   .AddCastMember(new Actor(Name.Create("Laurence", "Fishburne").Value)),

                   new Movie(
                    "Se7en",
                    "Se7en.jpeg",
                    "Two detectives, a rookie and a veteran, hunt a serial killer who uses the seven deadly sins as his motives.",
                    new DateTime(1995, 9, 14))
                   .AddCastMember(new Actor(Name.Create("Morgan", "Freeman").Value))
                   .AddCastMember(new Actor(Name.Create("Brad", "Pitt").Value)),

                   new Movie(
                    "The Silence of the Lambs",
                    "TheSilenceoftheLambs.jpeg",
                    "A young F.B.I. cadet must receive the help of an incarcerated and manipulative cannibal killer to help catch another serial killer, a madman who skins his victims.",
                    new DateTime(1991, 1, 30))
                   .AddCastMember(new Actor(Name.Create("Jodie", "Foster").Value))
                   .AddCastMember(new Actor(Name.Create("Anthony", "Hopkins").Value)),

                   new Movie(
                    "Saving Private Ryan",
                    "SavingPrivateRyan.jpeg",
                    "Following the Normandy Landings, a group of U.S. soldiers go behind enemy lines to retrieve a paratrooper whose brothers have been killed in action.",
                    new DateTime(1998, 7, 21))
                   .AddCastMember(new Actor(Name.Create("Tom", "Hanks").Value))
                   .AddCastMember(new Actor(Name.Create("Matt", "Damon").Value)),

                   new Movie(
                    "Interstellar",
                    "Interstellar.jpeg",
                    "A team of explorers travel through a wormhole in space in an attempt to ensure humanity's survival.",
                    new DateTime(2014, 10, 26))
                   .AddCastMember(new Actor(Name.Create("Matthew", "McConaughey").Value))
                   .AddCastMember(new Actor(Name.Create("Anne", "Hethaway").Value)),

            };
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger<ImdbDbContext> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
