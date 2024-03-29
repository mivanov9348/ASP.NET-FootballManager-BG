﻿namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class ContinentalCupConfiguration : IEntityTypeConfiguration<ContinentalCup>
    {
        public void Configure(EntityTypeBuilder<ContinentalCup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                   .WithMany(x => x.EuropeanCups)
                   .HasForeignKey(x => x.GameId);

            builder.HasMany(x => x.Teams)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Fixtures)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.VirtualTeams)
                   .WithOne(x => x.EuropeanCup)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Draws)
                    .WithOne(x => x.ContinentalCup)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
