namespace FootballManager.Infrastructure.Data.Configurations
{    
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Players)
             .WithOne(x => x.Position)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
