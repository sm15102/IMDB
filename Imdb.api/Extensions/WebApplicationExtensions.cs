using Imdb.Identity;
using Imdb.Identity.Enteties;
using Imdb.Persistence;
using IMDB.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Imdb.api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDbContext(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<ImdbDbContext>>();
                var context = services.GetRequiredService<ImdbDbContext>();

                try
                {
                    logger.LogInformation("Migrating database associated with context {DbContextName}", nameof(ImdbDbContext));

                    var retry = Policy.Handle<SqlException>()
                                .WaitAndRetry(new TimeSpan[]
                                {
                                    TimeSpan.FromSeconds(3),
                                    TimeSpan.FromSeconds(5),
                                    TimeSpan.FromSeconds(8),
                                });

                  
                    retry.Execute(() => InvokeSeeder(context, logger));

                    logger.LogInformation("Migrated database associated with context {DbContextName}", nameof(ImdbDbContext));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", nameof(ImdbDbContext));
                }
            }

            return app;
        }


        public static WebApplication AddIdentityUsers(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<ImdbIdentityDbContext>>();
                var context = services.GetRequiredService<ImdbIdentityDbContext>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                try
                {
                    logger.LogInformation("Adding users to context {DbContextName}", nameof(ImdbIdentityDbContext));

                    var retry = Policy.Handle<SqlException>()
                                .WaitAndRetry(new TimeSpan[]
                                {
                                    TimeSpan.FromSeconds(3),
                                    TimeSpan.FromSeconds(5),
                                    TimeSpan.FromSeconds(8),
                                });


                    retry.Execute(() => InvokeSeeder(context, userManager));

                    logger.LogInformation("Users added to context {DbContextName}", nameof(ImdbIdentityDbContext));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while adding users on context {DbContextName}", nameof(ImdbIdentityDbContext));
                }
            }

            return app;
        }


        private static void InvokeSeeder(ImdbDbContext context, ILogger<ImdbDbContext> logger)
        {
            context.Database.MigrateAsync().Wait();
            new ImdbDbContextSeed().SeedAsync(context, logger).Wait();
        }

        private static void InvokeSeeder(ImdbIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.MigrateAsync().Wait();
            UserCreator.SeedAsync(userManager).Wait();
        }

    }
}
