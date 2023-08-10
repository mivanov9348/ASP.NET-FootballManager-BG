namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Reflection.Emit;

    public class PlayerStatsConfiguration : IEntityTypeConfiguration<PlayerAttribute>
    {
        public void Configure(EntityTypeBuilder<PlayerAttribute> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Player)
                   .WithOne(p => p.PlayerAttributes)
                   .HasForeignKey<PlayerAttribute>(a => a.PlayerId);
        }
    }
}
