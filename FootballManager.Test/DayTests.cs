namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Common;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;
    public class DayTests : IDisposable
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
            .AddSingleton<IDayService, DayService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IDayService>();

        }

        [Test]
        public async Task CalculateDays()
        {
            var service = serviceProvider.GetService<IDayService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var game = new Game();
                context.Games.Add(game);
                var day = new Day
                {
                    GameId = game.Id
                };
                context.Days.Add(day);
                var day1 = new Day()
                {
                    GameId = game.Id
                };
                context.Days.Add(day1);
                context.SaveChanges();

                var days = await service.GetAllDays(game);

                Assert.AreEqual(2, days.Count);
            }
        }
        [Test]
        public async Task GetCurrentDay()
        {
            var service = serviceProvider.GetService<IDayService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var game = new Game
                {
                    Day = 5,
                    Year = 1
                };
                context.Games.Add(game);
                var day = new Day
                {
                    CurrentDay = 5,
                    Year = 1,
                    GameId = game.Id
                };
                context.Days.Add(day);
                var day1 = new Day()
                {
                    CurrentDay = 6,
                    Year = 1,
                    GameId = game.Id
                };
                context.Days.Add(day1);
                context.SaveChanges();

                var sadda = await service.GetCurrentDay(game);

                Assert.AreEqual(5, sadda.CurrentDay);
            }
        }

        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
