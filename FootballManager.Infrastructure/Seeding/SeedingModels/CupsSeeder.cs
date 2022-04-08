namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class CupsSeeder : ISeeder
    {
        public CupsSeeder()
        {
        }
        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Cups.Any())
            {
                Cup[] cups = new Cup[]
                {
                   new Cup() { Name = "Bulgarian Cup", Participants=32,Rounds=6,NationId = dbContext.Nations.FirstOrDefault(x=>x.Name=="Bulgaria").Id },
                };

                await dbContext.Cups.AddRangeAsync(cups);
            }
        }
    }
}
