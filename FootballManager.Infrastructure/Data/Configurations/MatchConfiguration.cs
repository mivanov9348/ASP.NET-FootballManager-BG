using ASP.NET_FootballManager.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballManager.Infrastructure.Data.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.CurrentFixture)
                 .WithMany(x => x.Matches)
                 .HasForeignKey(x => x.CurrentFixtureId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Game)
                 .WithMany(x => x.Matches)
                 .HasForeignKey(x => x.GameId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
