namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Player;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlayerTests : IDisposable
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
            .AddSingleton<IPlayerService, PlayerService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IPlayerService>();
        }

        [Test]
        public async void GetPlayerById()
        {
            var service = serviceProvider.GetService<IPlayerService>();

            var game = NewGame(1);
            var team = NewTeam(game);
            var player = NewPlayer(team, game, "Gosho", "Petrov");

            Assert.AreEqual(game.Id, player.GameId);
            var playerFirstName = await service.GetPlayerById(player.Id);
            Assert.AreEqual(player.FirstName, playerFirstName.FirstName);

        }
        [Test]
        public async Task GetRandomPlayerFromTeam()
        {
            var service = serviceProvider.GetService<IPlayerService>();

            var game = NewGame(2);
            var team = NewTeam(game);
            var player = NewPlayer(team, game, "Gosho", "Petkov");
            var player1 = NewPlayer(team, game, "Martin", "Petrov");
            var player2 = NewPlayer(team, game, "Ivan", "Ivanov");
            var player3 = NewPlayer(team, game, "Petur", "Stoyanov");
            var player4 = NewPlayer(team, game, "Hristo", "Kolev");

            var randomPlayer = await service.GetRandomPlayer(team);

            using (var context = new FootballManagerDbContext(options))
            {
                var rndPl = await Task.Run(()=>context.Players);
                Assert.IsTrue(rndPl.Where(x => x.TeamId == team.Id).Contains(randomPlayer));
            }
        }

        [Test]
        public async Task GetStartingPlayers()
        {
            var service = serviceProvider.GetService<IPlayerService>();

            var game = NewGame(2);
            var team = NewTeam(game);
            var player = NewPlayer(team, game, "Gosho", "Petkov");
            var player1 = NewPlayer(team, game, "Martin", "Petrov");
            var player2 = NewPlayer(team, game, "Ivan", "Ivanov");
            var player3 = NewPlayer(team, game, "Petur", "Stoyanov");
            var player4 = NewPlayer(team, game, "Hristo", "Kolev");

            var startingPlayers = await service.GetStartingEleven(team.Id);

            using (var context = new FootballManagerDbContext(options))
            {
                Assert.AreEqual(5, startingPlayers.Count);
            }
        }
        [Test]
        public async Task GetPlayersByTeam()
        {
            var service = serviceProvider.GetService<IPlayerService>();

            var game = NewGame(2);
            var team = NewTeam(game);
            var player = NewPlayer(team, game, "Gosho", "Petkov");
            var player1 = NewPlayer(team, game, "Martin", "Petrov");
            var player2 = NewPlayer(team, game, "Ivan", "Ivanov");
            var player3 = NewPlayer(team, game, "Petur", "Stoyanov");
            var player4 = NewPlayer(team, game, "Hristo", "Kolev");

            var players = await service.GetPlayersByTeam(team.Id);

            using (var context = new FootballManagerDbContext(options))
            {
                Assert.AreEqual(5, players.Count);
            }

        }

        public VirtualTeam NewTeam(Game currentgame)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var team = new VirtualTeam
                {
                    TeamId = 1,
                    GameId = currentgame.Id
                };
                context.VirtualTeams.Add(team);
                context.SaveChanges();
                return team;
            }
        }
        public Game NewGame(int id)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var Game = new Game
                {
                    TeamId = 1
                };
                context.Games.Add(Game);
                context.SaveChanges();
                return Game;
            }
        }
        public Player NewPlayer(VirtualTeam team, Game currentGame, string firstName, string lastName)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var newPlayer = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    TeamId = team.Id,
                    IsStarting11 = true,
                    Age = 20,
                    Attack = 50,
                    Defense = 60,
                    Speed = 70,
                    LeagueId = 1,
                    CityId = 1,
                    GameId = currentGame.Id,
                    NationId = 1
                };
                context.Players.Add(newPlayer);
                context.SaveChanges();
                return newPlayer;
            }
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
