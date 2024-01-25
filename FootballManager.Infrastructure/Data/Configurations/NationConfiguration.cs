namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class NationConfiguration : IEntityTypeConfiguration<Nation>
    {
        public void Configure(EntityTypeBuilder<Nation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Cities)
               .WithOne(x => x.Nation)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Teams)
             .WithOne(x => x.Nation)
             .OnDelete(DeleteBehavior.Restrict);
       

            builder.HasMany(x => x.Players)
             .WithOne(x => x.Nation)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Leagues)
               .WithOne(x => x.Nation)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Cups)
               .WithOne(x => x.Nation)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
