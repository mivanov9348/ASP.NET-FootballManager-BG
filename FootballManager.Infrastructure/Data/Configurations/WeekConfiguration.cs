namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;  
    public class WeekConfiguration : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Weeks)
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Year)
                   .WithMany(x => x.Weeks)
                   .HasForeignKey(x => x.YearId);

            builder.HasMany(x => x.Months)
                   .WithMany(x => x.Weeks);

            builder.HasMany(x => x.Days)
                   .WithOne(x => x.Week)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
