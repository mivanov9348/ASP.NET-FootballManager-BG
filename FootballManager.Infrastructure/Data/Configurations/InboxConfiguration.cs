namespace FootballManager.Infrastructure.Data.Configurations
{
    using ASP.NET_FootballManager.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public class InboxConfiguration : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Game)
                 .WithMany(x => x.Inboxes)
                 .HasForeignKey(x => x.GameId);
        }
    }
}
