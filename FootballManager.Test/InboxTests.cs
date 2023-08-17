using ASP.NET_FootballManager.Data;
using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using ASP.NET_FootballManager.Services.Inbox;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Test
{
    public class InboxTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private IInboxService service;
        private Game game;
        private Inbox inbox1;

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
            .AddSingleton<IInboxService, InboxService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IInboxService>();

          
        }

        [Test]
        public async Task GetInboxesMessages()
        {
            var inboxes = await service.GetInboxMessages(game.Id);
            Assert.AreEqual(1, inboxes.Count());
        }

        [Test]
        public async Task GetFullMessage()
        {
            var fullMess = await service.GetFullMessage(inbox1.Id, game);
            Assert.AreEqual("2", fullMess.FullMessage);
        }

     
        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
