namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nation)
                  .WithMany(x => x.Leagues)
                  .HasForeignKey(x => x.NationId);

            builder.HasOne(x => x.Draw)
                  .WithMany(x => x.Leagues)
                  .HasForeignKey(x => x.DrawId);

            builder.HasMany(x => x.Players)
                  .WithOne(x => x.League)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Teams)
                  .WithOne(x => x.League)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
                  .WithOne(x => x.League)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Fixtures)
                  .WithOne(x => x.League)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
