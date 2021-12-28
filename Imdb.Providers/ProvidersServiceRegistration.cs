using Imdb.Application.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Imdb.Providers
{
    public static class ProvidersServiceRegistration
    {
        public static IServiceCollection AddProvidsersServices(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<ISearchObjectProvider, SearchObjectProvider>();

            return services;
        }
    }
}
