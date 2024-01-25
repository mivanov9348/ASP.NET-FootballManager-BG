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

            Create(options);
        }

        [Test]
        public async Task GetInboxesMessages()
        {
            var inboxes = await service.GetInboxMessages(game.Id);
        }

        [Test]
        public async Task GetFullMessage()
        {
            var fullMess = await service.GetFullMessage(inbox1.Id, game);
        }

        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            service = serviceProvider.GetService<IInboxService>();

            using (var context = new FootballManagerDbContext(options))
            {
                game = new Game();
                context.Games.Add(game);
                inbox1 = new Inbox { Game = game, GameId = game.Id, MessageReview = "1", FullMessage = "2" };
                context.Inboxes.Add(inbox1);

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
