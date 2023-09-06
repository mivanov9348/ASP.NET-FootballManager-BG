namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nation)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.NationId);

            builder.HasOne(x => x.City)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.CityId);

            builder.HasOne(x => x.League)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.LeagueId);

            builder.HasMany(x => x.VirtualTeams)
                .WithOne(x => x.Team)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Games)
                .WithOne(x => x.Team)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Managers)
                .WithOne(x => x.CurrentTeam)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Cup)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.CupId);

            builder.HasOne(x => x.EuropeanCup)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.EuropeanCupId);
        }
    }
}
