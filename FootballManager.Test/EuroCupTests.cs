using ASP.NET_FootballManager.Data;
using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using ASP.NET_FootballManager.Services.EuroCup;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Test
{
    public class EuroCupTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private IEuroCupService service;
        private EuropeanCup cup;
        private VirtualTeam team1;
        private VirtualTeam team2;
        private Fixture fixture;
        private Day day;

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
            .AddSingleton<IEuroCupService, EuroCupService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IEuroCupService>();

            Create(options);
        }

        [Test]
        public async Task GetAllEuroCup()
        {
            var cups = await service.AllEuroCups();
            Assert.AreEqual(0, cups.Count());
        }
                
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            service = serviceProvider.GetService<IEuroCupService>();

            using (var context = new FootballManagerDbContext(options))
            {
                cup = new EuropeanCup { Name = "EuroCup1" };
            }
        }

        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
