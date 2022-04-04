using ASP.NET_FootballManager.Data;
using FootballManager.Infrastructure.Seeding.SeedingModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FootballManager.Infrastructure.Seeding
{
    public class FootballManagerDbContextSeeder : ISeeder
    {
        public FootballManagerDbContextSeeder()
        {


        }

        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(FootballManagerDbContextSeeder));

            var seeders = new List<ISeeder>
                          {
                              new NationsSeeder(),
                              new CitiesSeeder(),
                              new LeaguesSeeder(),
                              new EuropeanCupsSeeder(),
                              new CupsSeeder(),
                              new PositionsSeeder(),
                              new TeamsSeeder()
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }
        }
    }
}
