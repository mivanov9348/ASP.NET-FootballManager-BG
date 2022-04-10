namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class PositionsSeeder : ISeeder
    {
        public PositionsSeeder()
        {
        }
        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Positions.Any())
            {
                Position[] positions = new Position[]
                {
                   new Position() { Name = "Goalkeeper", Abbr="GK" },
                   new Position() { Name = "Defender", Abbr="DF" },
                   new Position() { Name = "Midlefielder", Abbr="MF" },
                   new Position() { Name = "Striker", Abbr="ST" }
                };

                await dbContext.Positions.AddRangeAsync(positions);
            }
        }
    }
}
