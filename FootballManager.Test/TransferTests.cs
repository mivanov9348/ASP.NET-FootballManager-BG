namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Transfer;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    public class TransferTests : IDisposable
    {

        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private ITransferService transferService;
        private Game game;
        private Player player;
        private Player player2;
        private VirtualTeam team;
        private Position position;

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
            .AddSingleton<ITransferService, TransferService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ITransferService>();

            Create(options);
        }

        [Test]
        public async Task GetAllFreeAgents()
        {
            var freeAgents = await transferService.GetAllFreeAgents(game.Id, 1, game);
        }

        [Test]
        public async Task GetCurrentTeamPlayers()
        {
            var players = await transferService.GetCurrentTeamPlayers(team.Id);
        }     

        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            transferService = serviceProvider.GetService<ITransferService>();
            using (var context = new FootballManagerDbContext(options))
            {
                game = new Game();
                context.Games.Add(game);
                position = new Position { Name = "Goalkeeper", Abbr = "GK" };
                context.Positions.Add(position);
                team = new VirtualTeam { Name = "GornoDolnishte", Game = game, GameId = game.Id };
                context.VirtualTeams.Add(team);
                player = new Player { FirstName = "Petur", LastName = "Georgiev", Position = position, Game = game, GameId = game.Id, FreeAgent = true };
                player2 = new Player { FirstName = "Georgi", LastName = "Petrov", Position = position, Game = game, GameId = game.Id, FreeAgent = false, TeamId = team.Id, Team = team };
                context.Players.Add(player);
                context.Players.Add(player2);

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
