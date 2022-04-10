namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class VirtualTeamsConfiguration : IEntityTypeConfiguration<VirtualTeam>
    {
        public void Configure(EntityTypeBuilder<VirtualTeam> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Team)
              .WithMany(x => x.VirtualTeams)
              .HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.Game)
              .WithMany(x => x.VirtualTeams)
              .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.League)
             .WithMany(x => x.VirtualTeams)
             .HasForeignKey(x => x.LeagueId);

            builder.HasOne(x => x.Cup)
             .WithMany(x => x.VirtualTeams)
             .HasForeignKey(x => x.CupId);

            builder.HasOne(x => x.EuropeanCup)
             .WithMany(x => x.VirtualTeams)
             .HasForeignKey(x => x.EuropeanCupId);

            builder.HasMany(x => x.Players)
               .WithOne(x => x.Team)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.HomeMatches)
              .WithOne(c => c.HomeTeam)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AwayMatches)
              .WithOne(c => c.AwayTeam)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
