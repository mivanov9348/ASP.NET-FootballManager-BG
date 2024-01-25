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

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.Draws)
                   .HasForeignKey(x => x.GameId);

            builder.HasOne(x => x.ContinentalCup)
                  .WithMany(x => x.Draws)
                  .HasForeignKey(x => x.ContinentalCupId);

            builder.HasOne(x => x.Cup)
                  .WithMany(x => x.Draws)
                  .HasForeignKey(x => x.CupId);

            builder.HasMany(x => x.Fixtures)
                   .WithOne(x => x.Draw)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Leagues)
                   .WithOne(x => x.Draw)
                   .OnDelete(DeleteBehavior.Restrict);            
        }
    }
}
