namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nation)
             .WithMany(x => x.Cities)
             .HasForeignKey(x => x.NationId);

            builder.HasMany(x => x.Players)
                     .WithOne(x => x.City)
                     .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Players)
                     .WithOne(x => x.City)
                     .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
