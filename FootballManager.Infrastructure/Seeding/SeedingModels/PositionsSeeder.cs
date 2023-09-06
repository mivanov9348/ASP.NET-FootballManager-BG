namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;

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
                   new Position() { Name = "Goalkeeper", Abbr="GK", Order = 1},
                   new Position() { Name = "Defender", Abbr="DF" , Order = 2},
                   new Position() { Name = "Midlefielder", Abbr="MF", Order = 3},
                   new Position() { Name = "Forward", Abbr="ST", Order = 4 }
                };

                await dbContext.Positions.AddRangeAsync(positions);
            }
        }
    }
}
