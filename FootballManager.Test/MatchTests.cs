namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Match;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;

    public class MatchTests : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private Day day;
        private Fixture fixture;
        private Player player;

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
            .AddSingleton<IMatchService, MatchService>()
            .BuildServiceProvider();

            serviceProvider.GetService<IMatchService>();
            Create(options);
        }

        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            throw new NotImplementedException();
        }

        [TearDown]
        public void Dispose()
        {
            connection.Close();
        }
    }
}
