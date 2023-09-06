namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class EuropeanCupConfiguration : IEntityTypeConfiguration<EuropeanCup>
    {
        public void Configure(EntityTypeBuilder<EuropeanCup> builder)
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
        }
    }
}
