namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.League;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
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
        private Cup cup;
        private VirtualTeam team1;
        private VirtualTeam team2;
        private Fixture fixture;
        private Day day;

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

            Create(options);
        }

        [Test]
        public async Task GetCurrentCup()
        {
            var service = serviceProvider.GetService<ICupService>();
            var cup = await Task.Run(() => service.GetCurrentCup());

            Assert.AreEqual("Cup1", cup.Name);

        }
        [Test]
        public async Task GetWinner()
        {
            var service = serviceProvider.GetService<ICupService>();
            await Task.Run(() => service.CheckWinner(fixture));

            Assert.AreEqual(fixture.AwayTeamId, fixture.WinnerTeamId);
        }
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                day = new Day();
                context.Days.Add(day);

                fixture = new Fixture
                {
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeTeamGoal = 1,
                    AwayTeamGoal = 2,
                    DayId = day.Id
                };
                context.Fixtures.Add(fixture);

                cup = new Cup
                {
                    Name = "Cup1"
                };
                context.Cups.Add(cup);

                team1 = new VirtualTeam
                {
                    CupId = cup.Id,
                    Name = "team1"
                };
                team2 = new VirtualTeam
                {
                    CupId = cup.Id,
                    Name = "team2"
                };
                context.VirtualTeams.Add(team1);
                context.VirtualTeams.Add(team2);

                context.SaveChanges();
            }
        }

        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
