namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Team)
              .WithMany(x => x.Games)
              .HasForeignKey(x => x.TeamId);

            builder.HasOne(x => x.Manager)
              .WithMany(x => x.Games)
              .HasForeignKey(x => x.ManagerId);

            builder.HasOne(a => a.GameOption)
                   .WithOne(p => p.Game)
                   .HasForeignKey<Game>(a => a.GameOptionId);

            builder.HasMany(x => x.Inboxes)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Days)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Matches)
               .WithOne(x => x.Game)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
