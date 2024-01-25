namespace FootballManager.Infrastructure.Seeding
{
    using ASP.NET_FootballManager.Data;
    public interface ISeeder
    {
        Task SeedAsync(FootballManagerDbContext dbContext, IServiceProvider serviceProvider);
    }
}
