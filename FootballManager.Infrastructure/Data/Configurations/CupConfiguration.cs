namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class CupConfiguration : IEntityTypeConfiguration<Cup>
    {
        public void Configure(EntityTypeBuilder<Cup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Cups)
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Nation)
               .WithMany(x => x.Cups)
               .HasForeignKey(x => x.NationId);

            builder.HasMany(x => x.Teams)
               .WithOne(x => x.Cup)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Fixtures)
               .WithOne(x => x.Cup)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
               .WithOne(x => x.Cup)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Draws)
                   .WithOne(x => x.Cup)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
