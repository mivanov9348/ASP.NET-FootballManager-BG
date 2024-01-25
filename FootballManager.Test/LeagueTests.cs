namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.League;
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
        private League league;
        private Game game;
        private VirtualTeam team1;
        private VirtualTeam team2;

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
            Create(options);
        }

        [Test]
        public async Task GetAllLeagues()
        {
            var service = serviceProvider.GetService<ILeagueService>();
            var allLeagues = await service.GetAllLeagues();
        }

        [Test]
        public async Task GetLeagueById()
        {
            var service = serviceProvider.GetService<ILeagueService>();
            var currentLeague = await service.GetLeague(league.Id);
        }

        [Test]
        public async Task GetStandings()
        {
            var service = serviceProvider.GetService<ILeagueService>();
            var standings = await service.GetStandingsByLeague(league.Id, game);

        }
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                league = new League
                {
                    Name = "League1"
                };
                context.Leagues.Add(league);
                game = new Game();
                context.Games.Add(game);
                team1 = new VirtualTeam
                {
                    Name = "ds",
                    GameId = game.Id,
                    Points = 10,
                    LeagueId = league.Id
                };
                context.VirtualTeams.Add(team1);
                team2 = new VirtualTeam
                {
                    Name = "ds",
                    GameId = game.Id,
                    Points = 12,
                    LeagueId = league.Id
                };
                context.VirtualTeams.Add(team2);
                context.SaveChanges();
            }
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
