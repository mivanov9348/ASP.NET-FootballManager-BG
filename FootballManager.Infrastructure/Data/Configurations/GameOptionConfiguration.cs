namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class GameOptionConfiguration : IEntityTypeConfiguration<GameOption>
    {
        public void Configure(EntityTypeBuilder<GameOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<IdentityUser>()
                   .WithOne()
                   .HasForeignKey<GameOption>(x => x.UserId);

            builder.HasMany(x => x.Games)
                   .WithOne(x => x.GameOption)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
