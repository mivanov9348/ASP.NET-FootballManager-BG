using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FootballManager.Infrastructure.Data.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nation)
              .WithMany(x => x.Players)
              .HasForeignKey(x => x.NationId);

            builder.HasOne(x => x.City)
              .WithMany(x => x.Players)
              .HasForeignKey(x => x.CityId);

            builder.HasOne(x => x.Game)
              .WithMany(x => x.Players)
              .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Team)
              .WithMany(x => x.Players)
              .HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.League)
              .WithMany(x => x.Players)
              .HasForeignKey(x => x.LeagueId);

            builder.HasOne(p => p.PlayerAttributes)
                   .WithOne(a => a.Player)
                   .HasForeignKey<PlayerAttribute>(a => a.PlayerId);

            builder.HasOne(p => p.PlayerStats)
                  .WithOne(a => a.Player)
                  .HasForeignKey<PlayerStats>(a => a.PlayerId);
        }
    }
}
