namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class EuropeanCupsSeeder : ISeeder
    {
        public EuropeanCupsSeeder()
        {
        }

        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (!dbContext.EuropeanCups.Any())
            {
                EuropeanCup[] europeanCups = new EuropeanCup[]
                {
                   new EuropeanCup() { Name = "Champions Cup", Participants=32,Rounds=5,Rank=1 },
                    new EuropeanCup() { Name = "Euro Cup", Participants=32,Rounds=5,Rank=2 }
                };

                await dbContext.EuropeanCups.AddRangeAsync(europeanCups);
            }
        }
    }
}
