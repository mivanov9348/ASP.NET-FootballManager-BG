namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System.Reflection.Emit;

    public class AttributeConfiguration : IEntityTypeConfiguration<PlayerAttribute>
    {
        public void Configure(EntityTypeBuilder<PlayerAttribute> builder)
        {
            builder.HasKey(x => x.PlayerId);

            builder.HasOne(a => a.Player)
                   .WithOne(p => p.Attributes)
                   .HasForeignKey<PlayerAttribute>(a => a.PlayerId);
        }
    }
}
