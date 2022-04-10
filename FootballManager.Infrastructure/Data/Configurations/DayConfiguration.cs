namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
               .WithMany(x => x.Days)
               .HasForeignKey(x => x.GameId);

            builder.HasMany(x => x.Fixtures)
               .WithOne(x => x.Day)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
