namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class MonthConfiguration : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                  .WithMany(x => x.Months)
                  .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.Year)
                   .WithMany(x => x.Months)
                   .HasForeignKey(x => x.YearId);

            builder.HasMany(x => x.Weeks)
                   .WithMany(x => x.Months);                   

            builder.HasMany(x => x.Days)
                   .WithOne(x => x.Month)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
