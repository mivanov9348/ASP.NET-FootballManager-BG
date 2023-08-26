using FootballManager.Infrastructure.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Infrastructure.Data.Configurations
{
    public class DrawConfiguration : IEntityTypeConfiguration<Draw>
    {
        public void Configure(EntityTypeBuilder<Draw> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Fixtures)
                   .WithOne(x => x.Draw)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Leagues)
                   .WithOne(x => x.Draw)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
