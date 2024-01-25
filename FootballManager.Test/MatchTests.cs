namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Match;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MatchTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private IMatchService service;
        private Game game;
        private Day day;
        private Fixture fixture;
        private VirtualTeam team1;
        private VirtualTeam team2;
        private Player player;
        private Match match;

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
            .AddSingleton<IMatchService, MatchService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IMatchService>();
            Create(options);
        }

        [Test]
        public async Task GetCurrentMatch()
        {
            var currentMatch = await service.GetCurrentMatch(match.Id);
        }

        [Test]
        public async Task GetStarting11()
        {
            var starting11Players = await service.GetStarting11(team1.Id);
        }
                
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            service = serviceProvider.GetService<IMatchService>();

            using (var context = new FootballManagerDbContext(options))
            {
                day = new Day { CurrentDay = 1, Year = 1 };
                context.Days.Add(day);
                game = new Game { Day = day.CurrentDay, Year = day.Year };
                context.Games.Add(game);
                team1 = new VirtualTeam { Game = game, GameId = game.Id, Name = "Team1" };
                team2 = new VirtualTeam { Game = game, GameId = game.Id, Name = "Team2" };
                context.VirtualTeams.Add(team1);
                context.VirtualTeams.Add(team2);
                player = new Player { Team = team1, TeamId = team1.Id, FirstName = "Martin", LastName = "Ivanov", Game = game, GameId = game.Id, IsStarting11 = true };
                context.Players.Add(player);
                fixture = new Fixture { Day = day, DayId = day.Id, HomeTeamId = team1.Id, AwayTeamId = team2.Id, GameId = game.Id };
                context.Fixtures.Add(fixture);
                match = new Match { Game = game, GameId = game.Id, CurrentFixture = fixture, CurrentFixtureId = fixture.Id };
                context.Matches.Add(match);

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
