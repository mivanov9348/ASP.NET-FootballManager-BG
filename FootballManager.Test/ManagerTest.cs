namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Manager;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    public class ManagerTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private Manager manager;
        private IdentityUser user;
        private Team team;

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
            .AddSingleton<IManagerService, ManagerService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IManagerService>();
            Create(options);
        }

        [Test]
        public async Task GetCurrentManager()
        {
            var service = serviceProvider.GetService<IManagerService>();

            var id = manager.Id;
            var returningManagerId = await Task.Run(() => service.GetCurrentManager(user.Id));
            Assert.AreEqual(id, returningManagerId.Id);
        }
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                team = new Team { Name = "CSKA" };
                context.Teams.Add(team);
                user = new IdentityUser { Id = "1" };
                context.Users.Add(user);
                manager = new Manager
                {
                    FirstName = "Martin",
                    LastName = "Ivanov",
                    BornDate = DateTime.Now,              
                    CurrentTeam = team,
                    CurrentTeamId = team.Id,
                    User = user,
                    UserId = user.Id
                };
                context.Managers.Add(manager);
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

