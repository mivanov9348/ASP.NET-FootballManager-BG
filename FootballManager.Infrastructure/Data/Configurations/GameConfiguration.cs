namespace FootballManager.Infrastructure.Data.Configurations
{  
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Identity;
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

            builder.HasOne(x => x.GameOption)
                   .WithMany(x => x.Games)
                   .HasForeignKey(x => x.GameOptionId);

            builder.HasOne<IdentityUser>()
                  .WithOne()
                  .HasForeignKey<Game>(x => x.UserId);

            builder.HasMany(x => x.Inboxes)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Players)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Years)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Months)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Days)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Matches)
               .WithOne(x => x.Game)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.EuropeanCups)
                   .WithOne(x => x.Game)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
