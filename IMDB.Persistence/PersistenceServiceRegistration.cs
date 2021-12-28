using Imdb.Application.Contracts.Persistence;
using Imdb.Persistence.Repositories;
using IMDB.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImdbDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ImdbDbContextConnectionString"));
            });

            services.AddScoped<IMovieRepository, MovieRepository>();

            return services;
        }
    }
}
