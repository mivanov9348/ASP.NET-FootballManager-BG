namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.Team;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;

    public class TeamTests : IDisposable
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
            .AddSingleton<ITeamService, TeamService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ITeamService>();
        }

        [Test]
        public void CalculateTeamOverall()
        {
            var service = serviceProvider.GetService<ITeamService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newteam = new VirtualTeam
                {
                    Name = "CSKA"
                };
                context.VirtualTeams.Add(newteam);
                var newPlayer = new Player
                {
                    FirstName = "Hulk",
                    LastName = "Hogan",
                    TeamId = newteam.Id,
                    Attack = 50,
                    Defense = 60,
                    Speed = 100,
                    Overall = 70
                };
                context.Players.Add(newPlayer);
                context.SaveChanges();
                service.CalculateTeamOverall(newteam);
                Assert.AreEqual(70, newteam.Overall);

            }

        }
        [Test]
        public void GetAllTeams()
        {
            var service = serviceProvider.GetService<ITeamService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var game = new Game();
                var newteam = new VirtualTeam
                {
                    Name = "CSKA",
                    GameId = game.Id
                };
                context.VirtualTeams.Add(newteam);
                context.SaveChanges();

                Assert.AreEqual(4, service.GetAllVirtualTeams(game).Count);

            }

        }
        [Test]
        public void GetTeamById()
        {
            var service = serviceProvider.GetService<ITeamService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var newteam = new VirtualTeam
                {
                    Name = "dasdas"
                };
                context.VirtualTeams.Add(newteam);

                context.SaveChanges();

                Assert.AreEqual(newteam.Id, service.GetTeamById(newteam.Id).Id);

            }

        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
