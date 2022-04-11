namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Common;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class CommonTest : IDisposable
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
            .AddSingleton<ICommonService, CommonService>()
            .BuildServiceProvider();

            serviceProvider.GetService<ICommonService>();
        }

        public void Dispose()
        {
            connection.Close();
        }

        [Test]
        public async Task PositionsTest()
        {
            var service = serviceProvider.GetService<ICommonService>();

            using (var context = new FootballManagerDbContext(options))
            {
                var position = new Position { Name = "Striker", Abbr = "ST" };
                context.Positions.Add(position);
                context.SaveChanges();

                var positions = await service.GetAllPositions();

                Assert.AreEqual(1, context.Positions.Count());
                Assert.AreEqual(1, positions.Where(x => x.Name == "Striker").ToList().Count());
            }
        }
        [Test]
        public void NationsTest()
        {
            var service = serviceProvider.GetService<ICommonService>();

            using (var context = new FootballManagerDbContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    var nation = new Nation
                    {
                        Name = "Bolivia",
                        Abbr = "BOL"
                    };

                    context.Nations.Add(nation);
                    context.SaveChanges();
                }

                Assert.AreEqual(10, context.Nations.Count());

            }
        }
        [Test]
        public void CitiesTest()
        {
            var service = serviceProvider.GetService<ICommonService>();

            using (var context = new FootballManagerDbContext(options))
            {

                var city = new City
                {
                    Name = "Pregrad",
                    NationId = 1
                };

                Assert.DoesNotThrow(() => context.Cities.Add(city));

                context.Cities.Add(city);
                context.SaveChanges();
                Assert.AreEqual(1, context.Cities.Count());

            }
        }
        
    }


}
