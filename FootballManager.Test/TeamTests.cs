namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Team;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TeamTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private VirtualTeam team;
        private Player player;
        private Game game;

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
            .AddSingleton<ITeamService, TeamService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ITeamService>();

            Create(options);
        }

        [Test]
        public void CalculateTeamOverall()
        {
            var service = serviceProvider.GetService<ITeamService>();
            service.CalculateTeamOverall(team);
            Assert.AreEqual(70, team.Overall);
        }

        [Test]
        public async Task GetAllTeams()
        {
            var service = serviceProvider.GetService<ITeamService>();
            var allTeams = await service.GetAllVirtualTeams(game);
            Assert.AreEqual(1, allTeams.Count);
        }

        [Test]
        public async Task GetTeamById()
        {
            var service = serviceProvider.GetService<ITeamService>();
            var currentTeam = await service.GetTeamById(team.Id);
            Assert.AreEqual(team.Id, currentTeam.Id);
        }
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                game = new Game();
                context.Games.Add(game);
                team = new VirtualTeam
                {
                    Name = "CSKA",
                    Game = game,
                    GameId = game.Id
                };
                context.VirtualTeams.Add(team);
                player = new Player
                {
                    FirstName = "Hulk",
                    LastName = "Hogan",
                    TeamId = team.Id,                   
                    Overall = 70
                };
                context.Players.Add(player);
                context.SaveChanges();
            }
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
