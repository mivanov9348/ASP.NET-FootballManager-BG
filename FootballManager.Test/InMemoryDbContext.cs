using ASP.NET_FootballManager.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace FootballManager.Test
{
    public class InMemoryDbContext
    {
        private readonly SqliteConnection connection;
        private readonly DbContextOptions<FootballManagerDbContext> dbContextOptions;

        public InMemoryDbContext()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            dbContextOptions = new DbContextOptionsBuilder<FootballManagerDbContext>()
           .UseSqlite(connection).Options;

            using var context = new FootballManagerDbContext(dbContextOptions);

            context.Database.EnsureCreated();
        }
        public FootballManagerDbContext CreateContext() => new FootballManagerDbContext(dbContextOptions);

        public void Dispose() => connection.Dispose();

    }
}
