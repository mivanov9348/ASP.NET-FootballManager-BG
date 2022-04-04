namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;

    public class NationsSeeder : ISeeder
    {
        public NationsSeeder()
        {
        }

        public async Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Nations.Any())
            {
                Nation[] nations = new Nation[]
                {
                    new Nation() { Name = "Bulgaria", Abbr="BUL" },
                    new Nation() { Name = "Germany", Abbr="GER" },
                    new Nation() { Name = "Spain", Abbr="SPA" },
                    new Nation() { Name = "Italy", Abbr="ITA" },
                    new Nation() { Name = "Portugal", Abbr="POR" },
                    new Nation() { Name = "Serbia", Abbr="SER" },
                    new Nation() { Name = "Netherlands", Abbr="NED" },
                    new Nation() { Name = "England", Abbr="ENG" },
                    new Nation() { Name = "Greece", Abbr="BUL" },
                    new Nation() { Name = "France", Abbr="BUL" },
                    new Nation() { Name = "Congo", Abbr="BUL" },
                    new Nation() { Name = "Brazil", Abbr="BUL" },
                    new Nation() { Name = "Argentina", Abbr="BUL" },
                    new Nation() { Name = "Mexico", Abbr="BUL" },
                    new Nation() { Name = "United States", Abbr="USA" },
                    new Nation() { Name = "Nigeria", Abbr="BUL" },
                    new Nation() { Name = "Turkey", Abbr="BUL" },
                    new Nation() { Name = "Colombia", Abbr="BUL" },
                    new Nation() { Name = "Cameroon", Abbr="BUL" },
                    new Nation() { Name = "Romania", Abbr="BUL" },
                    new Nation() { Name = "Ukraine", Abbr="BUL" },
                    new Nation() { Name = "Belgium", Abbr="BUL" },
                    new Nation() { Name = "Sweden", Abbr="BUL" },
                    new Nation() { Name = "Norway", Abbr="BUL" },
                    new Nation() { Name = "Russia", Abbr="BUL" },
                    new Nation() { Name = "Scotland", Abbr="BUL" },
                    new Nation() { Name = "Israel", Abbr="BUL" },
                    new Nation() { Name = "Azerbaidjan", Abbr="BUL" },
                    new Nation() { Name = "Switzerland", Abbr="BUL" },
                    new Nation() { Name = "Austria", Abbr="AUS" },
                    new Nation() { Name = "Croatia", Abbr="CRO" },
                    new Nation() { Name = "Belarus", Abbr="BRS" },
                    new Nation() { Name = "Czech Republic", Abbr="CZE" },
                    new Nation() { Name = "Slovenia", Abbr="SLO" },
                    new Nation() { Name = "Slovakia", Abbr="SVK" }
                };

                await dbContext.Nations.AddRangeAsync(nations);
            }
        }
    }
}
