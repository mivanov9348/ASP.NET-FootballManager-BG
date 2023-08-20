namespace FootballManager.Infrastructure.Data.Configurations
{  
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CurrentTeam)
              .WithMany(x => x.Managers)
              .HasForeignKey(x => x.CurrentTeamId);

            builder.HasMany(x => x.Games)
              .WithOne(x => x.Manager)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<IdentityUser>()
                   .WithOne()
                   .HasForeignKey<Manager>(x => x.UserId);
        }
    }
}
