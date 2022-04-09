namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.League;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CupTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            connection = new SqliteConnection("datasource=:memory:");
            connection.Open();

            options = new DbContextOptionsBuilder<FootballManagerDbContext>()
                 .UseInMemoryDatabase(databaseName: "FootballManager")
                     .Options;

            serviceProvider = serviceCollection
            .AddSingleton(x => new FootballManagerDbContext(options))
            .AddSingleton<ICupService, CupService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ICupService>();


        }

        [Test]
        public async Task GetCurrentCup()
        {
            var service = serviceProvider.GetService<ICupService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newCup = new Cup
                {
                    Name = "Cup1"
                };
                context.Cups.Add(newCup);
                var team1 = new VirtualTeam
                {
                    CupId = newCup.Id,
                    Name = "team1"
                };
                var team2 = new VirtualTeam
                {
                    CupId = newCup.Id,
                    Name = "team2"
                };
                context.VirtualTeams.Add(team1);
                context.VirtualTeams.Add(team2);
                context.SaveChanges();

                var cup = await Task.Run(() => service.GetCurrentCup());
                Assert.AreEqual("Cup1", cup.Name);
            }
        }
        [Test]
        public void GetWinner()
        {
            var service = serviceProvider.GetService<ICupService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newDay = new Day();
                context.Days.Add(newDay);
                var newfixture = new Fixture
                {
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeTeamGoal = 1,
                    AwayTeamGoal = 2,
                    DayId = newDay.Id
                };
                context.Fixtures.Add(newfixture);
                context.SaveChanges();

                service.CheckWinner(newfixture);
                Assert.AreEqual(newfixture.AwayTeamId, newfixture.WinnerTeamId);



            }
        }

        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
