namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Month)
                  .WithMany(x => x.Days)
                  .HasForeignKey(x => x.MonthId);

            builder.HasOne(x => x.Year)
                   .WithMany(x => x.Days)
                   .HasForeignKey(x => x.YearId);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Days)
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Week)
                   .WithMany(x => x.Days)
                   .HasForeignKey(x => x.WeekId);

            builder.HasMany(x => x.Fixtures)
                   .WithOne(x => x.Day)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
