namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class EuropeanCupConfiguration : IEntityTypeConfiguration<EuropeanCup>
    {
        public void Configure(EntityTypeBuilder<EuropeanCup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Teams)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Fixtures)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
