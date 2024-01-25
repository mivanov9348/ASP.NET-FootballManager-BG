namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class YearConfiguration : IEntityTypeConfiguration<Year>
    {
        public void Configure(EntityTypeBuilder<Year> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                  .WithMany(x => x.Years)
                  .HasForeignKey(x => x.GameId);

            builder.HasMany(x => x.Months)
                   .WithOne(x => x.Year)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Weeks)
                   .WithOne(x => x.Year)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Days)
                   .WithOne(x => x.Year)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
