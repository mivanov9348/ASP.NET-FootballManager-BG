namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;

    public class CitiesSeeder : ISeeder
    {
        public CitiesSeeder()
        {

        }
        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            var bulgaria = dbContext.Nations.FirstOrDefault(x => x.Name == "Bulgaria");

            if (!dbContext.Cities.Any())
            {
                City[] cities = new City[]
                {
                   new City() { Name = "Sliven", NationId=bulgaria.Id },
                   new City() { Name = "Sofia", NationId=bulgaria.Id },
                   new City() { Name = "Plovdiv", NationId=bulgaria.Id },
                   new City() { Name = "Varna", NationId=bulgaria.Id },
                   new City() { Name = "Burgas", NationId=bulgaria.Id },
                   new City() { Name = "Gabrovo", NationId=bulgaria.Id },
                   new City() { Name = "Pleven", NationId=bulgaria.Id },
                   new City() { Name = "Haskovo", NationId=bulgaria.Id },
                   new City() { Name = "Stara Zagora", NationId=bulgaria.Id },
                   new City() { Name = "Nova Zagora", NationId=bulgaria.Id },
                   new City() { Name = "Yambol", NationId=bulgaria.Id },
                   new City() { Name = "Ruse", NationId=bulgaria.Id },
                   new City() { Name = "Blagoevgrad", NationId=bulgaria.Id },
                   new City() { Name = "Shumen", NationId=bulgaria.Id },
                   new City() { Name = "Balchik", NationId=bulgaria.Id },
                   new City() { Name = "Veliko Turnovo", NationId=bulgaria.Id },
                   new City() { Name = "Vidin", NationId=bulgaria.Id },
                   new City() { Name = "Kavarna", NationId=bulgaria.Id },
                   new City() { Name = "Tryavna", NationId=bulgaria.Id },
                   new City() { Name = "Montana", NationId=bulgaria.Id },
                   new City() { Name = "Smolyan", NationId=bulgaria.Id },
                   new City() { Name = "Pazardzhik", NationId=bulgaria.Id },
                   new City() { Name = "Pernik", NationId=bulgaria.Id },
                   new City() { Name = "Kyustendil", NationId=bulgaria.Id },
                   new City() { Name = "Lovech", NationId=bulgaria.Id },
                   new City() { Name = "Gorna Oryahovitsa", NationId=bulgaria.Id },
                   new City() { Name = "Sevlievo", NationId=bulgaria.Id },
                   new City() { Name = "Karlovo", NationId=bulgaria.Id },
                   new City() { Name = "Kalofer", NationId=bulgaria.Id },
                   new City() { Name = "Elhovo", NationId=bulgaria.Id },
                   new City() { Name = "Radomir", NationId=bulgaria.Id },
                   new City() { Name = "Veliki Preslav", NationId=bulgaria.Id },
                   new City() { Name = "Sopot", NationId=bulgaria.Id },
                   new City() { Name = "Etropole", NationId=bulgaria.Id },
                   new City() { Name = "Nesebar", NationId=bulgaria.Id }
                };

                await dbContext.Cities.AddRangeAsync(cities);
            }
        }
    }
}
