using ASP.NET_FootballManager.Data;
using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using ASP.NET_FootballManager.Services.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace FootballManager.Test
{
 
    public class CommonTest : IDisposable
    {
        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;
        private Position position;
        private Nation nation;
        private City city;

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
            .AddSingleton<ICommonService, CommonService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ICommonService>();

            Create(options);
        }
               
        [Test]
        public async Task PositionsTest()
        {
            var service = serviceProvider.GetService<ICommonService>();
            var positions = await service.GetAllPositions();

        }
        [Test]
        public async Task NationsTest()
        {
            var service = serviceProvider.GetService<ICommonService>();
            var nations = await service.GetAllNations();
        }

        [Test]
        public async Task CitiesTest()
        {
            var service = serviceProvider.GetService<ICommonService>();
            var cities = await service.GetAllCities();

        }
        private void Create(DbContextOptions<FootballManagerDbContext> options)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                position = new Position { Name = "Striker", Abbr = "ST" };
                context.Positions.Add(position);
                nation = new Nation { Name = "Bolivia", Abbr = "BOL" };
                context.Nations.Add(nation);
                city = new City { Name = "Pregrad", NationId = 1 };
                context.Cities.Add(city);

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            connection.Close();
        }

    }


}
