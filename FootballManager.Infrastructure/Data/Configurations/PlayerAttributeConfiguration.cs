namespace FootballManager.Infrastructure.Data.Configurations
{   
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
  
    public class PlayerAttributeConfiguration : IEntityTypeConfiguration<PlayerStats>
    {
        public void Configure(EntityTypeBuilder<PlayerStats> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Player)
                   .WithOne(p => p.PlayerStats)
                   .HasForeignKey<PlayerStats>(a => a.PlayerId);
        }
    }
}
