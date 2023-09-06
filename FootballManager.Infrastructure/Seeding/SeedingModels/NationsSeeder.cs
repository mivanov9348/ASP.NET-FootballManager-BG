namespace FootballManager.Infrastructure.Seeding.SeedingModels
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
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
                    new Nation() { Name = "Greece", Abbr="GRE" },
                    new Nation() { Name = "France", Abbr="FRA" },
                    new Nation() { Name = "Congo", Abbr="CON" },
                    new Nation() { Name = "Brazil", Abbr="BRA" },
                    new Nation() { Name = "Argentina", Abbr="ARG" },
                    new Nation() { Name = "Mexico", Abbr="MEX" },
                    new Nation() { Name = "United States", Abbr="USA" },
                    new Nation() { Name = "Nigeria", Abbr="NIG" },
                    new Nation() { Name = "Turkey", Abbr="TUR" },
                    new Nation() { Name = "Colombia", Abbr="COL" },
                    new Nation() { Name = "Cameroon", Abbr="CAM" },
                    new Nation() { Name = "Romania", Abbr="ROM" },
                    new Nation() { Name = "Ukraine", Abbr="UKR" },
                    new Nation() { Name = "Belgium", Abbr="BEL" },
                    new Nation() { Name = "Sweden", Abbr="SWE" },
                    new Nation() { Name = "Norway", Abbr="NOR" },
                    new Nation() { Name = "Russia", Abbr="RUS" },
                    new Nation() { Name = "Scotland", Abbr="SCO" },
                    new Nation() { Name = "Israel", Abbr="ISR" },
                    new Nation() { Name = "Azerbaidjan", Abbr="AZE" },
                    new Nation() { Name = "Switzerland", Abbr="SWI" },
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
