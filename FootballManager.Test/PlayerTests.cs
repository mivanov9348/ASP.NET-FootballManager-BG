namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
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

<<<<<<< HEAD
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
        public async Task GetPlayerById()
        {
            var service = serviceProvider.GetService<IPlayerService>();

            var game = NewGame(1);
            var team = NewTeam(game);
            var player = NewPlayer(team, game, "Gosho", "Petrov");
      
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
                var rndPl = await Task.Run(() => context.Players);
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
            }
        }
=======
      
>>>>>>> 1aede7b505d42a4d334ef6003d735f73c6c43338
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
