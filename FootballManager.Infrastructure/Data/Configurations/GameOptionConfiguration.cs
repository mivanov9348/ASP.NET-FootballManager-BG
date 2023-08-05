namespace FootballManager.Infrastructure.Data.Configurations
{
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class GameOptionConfiguration : IEntityTypeConfiguration<GameOption>
    {
        public void Configure(EntityTypeBuilder<GameOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(a => a.Game)
                   .WithOne(p => p.GameOption)
                   .HasForeignKey<GameOption>(a => a.GameId);
        }
    }
}
