namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;   
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class LeaguesSeeder : ISeeder
    {
        public LeaguesSeeder()
        {

        }
        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            var bulgaria = dbContext.Nations.FirstOrDefault(x => x.Name == "Bulgaria");
            if (!dbContext.Leagues.Any())
            {
                League[] leagues = new League[]
                {
                   new League() { Name = "First Bulgarian League", Level=1,NationId=bulgaria.Id,Rounds=15 },
                   new League() { Name = "Second Bulgarian League", Level=2,NationId=bulgaria.Id,Rounds=15 },
                   new League() { Name = "Third Bulgarian League", Level=3,NationId=bulgaria.Id,Rounds=15 },
                   new League() { Name = "Fourth Bulgarian League", Level=4,NationId=bulgaria.Id,Rounds=15 }
                };

                await dbContext.Leagues.AddRangeAsync(leagues);
            }
        }
    }
}
