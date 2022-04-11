namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;   
    public class FixtureConfiguration : IEntityTypeConfiguration<Fixture>
    {
        public void Configure(EntityTypeBuilder<Fixture> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.HomeTeam)
                     .WithMany(x => x.HomeMatches)
                     .HasForeignKey(x => x.HomeTeamId)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AwayTeam)
                    .WithMany(x => x.AwayMatches)
                    .HasForeignKey(x => x.AwayTeamId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.League)
                   .WithMany(x => x.Fixtures)
                   .HasForeignKey(x => x.LeagueId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Matches)
                   .WithOne(x => x.CurrentFixture)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cup)
                   .WithMany(x => x.Fixtures)
                   .HasForeignKey(x => x.CupId);

            builder.HasOne(x => x.EuropeanCup)
                   .WithMany(x => x.Fixtures)
                   .HasForeignKey(x => x.EuropeanCupId);

            builder.HasOne(x => x.Day)
                   .WithMany(x => x.Fixtures)
                   .HasForeignKey(x => x.DayId);
        }
    }
}
