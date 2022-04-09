namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LeagueTests : IDisposable
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
            .AddSingleton<ILeagueService, LeagueService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ILeagueService>();
        }

        [Test]
        public async Task GetAllLeagues()
        {
            var service = serviceProvider.GetService<ILeagueService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newLeague = new League
                {
                    Name = "League1"
                };
                context.Leagues.Add(newLeague);
                context.SaveChanges();

                var allLeagues = await service.GetAllLeagues();

                Assert.AreEqual(1, allLeagues.Count());
            }



        }
        [Test]
        public void GetLeagueById()
        {
            var service = serviceProvider.GetService<ILeagueService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newLeague = new League
                {
                    Name = "League1"
                };
                context.Leagues.Add(newLeague);
                context.SaveChanges();

                var currentLeague = service.GetLeague(newLeague.Id);

                Assert.AreEqual(newLeague.Id, currentLeague.Id);
            }



        }
        [Test]
        public async Task GetStandings()
        {
            var service = serviceProvider.GetService<ILeagueService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newGame = new Game();
                context.Games.Add(newGame);
                var newLeague = new League
                {
                    Name = "League1"
                };
                context.Leagues.Add(newLeague);
                var teamOne = new VirtualTeam
                {
                    Name = "ds",
                    GameId = newGame.Id,
                    Points = 10,
                    LeagueId = newLeague.Id
                };
                context.VirtualTeams.Add(teamOne);
                var teamTwo = new VirtualTeam
                {
                    Name = "ds",
                    GameId = newGame.Id,
                    Points = 12,
                    LeagueId = newLeague.Id
                };
                context.VirtualTeams.Add(teamTwo);
                context.SaveChanges();

                var standings = await service.GetStandingsByLeague(newLeague.Id, newGame);

                Assert.AreEqual(teamTwo.Name, standings.First().Name);
            }



        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
