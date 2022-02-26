﻿namespace ASP.NET_FootballManager.Data
{
    using ASP.NET_FootballManager.Data.DataModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : IdentityDbContext
    {
        public DbSet<Nation> Nations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<VirtualTeam> VirtualTeams { get; set; }


        public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
            : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Nation>(nat =>
            {
                nat.HasKey(x => x.Id);

                nat.HasMany(x => x.Cities)
                   .WithOne(x => x.Nation)
                   .OnDelete(DeleteBehavior.Restrict);

                nat.HasMany(x => x.Teams)
                 .WithOne(x => x.Nation)
                 .OnDelete(DeleteBehavior.Restrict);

                nat.HasMany(x => x.Managers)
                 .WithOne(x => x.Nation)
                 .OnDelete(DeleteBehavior.Restrict);

                nat.HasMany(x => x.Players)
                 .WithOne(x => x.Nation)
                 .OnDelete(DeleteBehavior.Restrict);

                nat.HasMany(x => x.Leagues)
                   .WithOne(x => x.Nation)
                   .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<City>(cit =>
            {
                cit.HasKey(x => x.Id);

                cit.HasOne(x => x.Nation)
                 .WithMany(x => x.Cities)
                 .HasForeignKey(x => x.NationId);

                cit.HasMany(x => x.Players)
                 .WithOne(x => x.City)
                 .OnDelete(DeleteBehavior.Restrict);

                cit.HasMany(x => x.Players)
                 .WithOne(x => x.City)
                 .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Team>(team =>
            {
                team.HasKey(x => x.Id);

                team.HasOne(x => x.Nation)
                    .WithMany(x => x.Teams)
                    .HasForeignKey(x => x.NationId);

                team.HasOne(x => x.City)
                    .WithMany(x => x.Teams)
                    .HasForeignKey(x => x.CityId);

                team.HasOne(x => x.League)
                    .WithMany(x => x.Teams)
                    .HasForeignKey(x => x.LeagueId);

                team.HasMany(x => x.Managers)
                    .WithOne(x => x.CurrentTeam)
                    .OnDelete(DeleteBehavior.Restrict);

                team.HasMany(x => x.Players)
                    .WithOne(x => x.Team)
                    .OnDelete(DeleteBehavior.Restrict);

                team.HasMany(x => x.VirtualTeams)
                    .WithOne(x => x.Team)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Position>(pos =>
            {
                pos.HasKey(x => x.Id);

                pos.HasMany(x => x.Players)
                 .WithOne(x => x.Position)
                 .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<League>(league =>
            {
                league.HasKey(x => x.Id);

                league.HasOne(x => x.Nation)
                      .WithMany(x => x.Leagues)
                      .HasForeignKey(x => x.NationId);

                league.HasMany(x => x.Players)
                      .WithOne(x => x.League)
                      .OnDelete(DeleteBehavior.Restrict);

                league.HasMany(x => x.Teams)
                      .WithOne(x => x.League)
                      .OnDelete(DeleteBehavior.Restrict);

            });

            builder.Entity<Player>(pl =>
            {
                pl.HasKey(x => x.Id);

                pl.HasOne(x => x.Nation)
                  .WithMany(x => x.Players)
                  .HasForeignKey(x => x.NationId);

                pl.HasOne(x => x.City)
                  .WithMany(x => x.Players)
                 .HasForeignKey(x => x.CityId);

                pl.HasOne(x => x.Team)
                  .WithMany(x => x.Players)
                  .HasForeignKey(x => x.TeamId);

                pl.HasOne(x => x.League)
                  .WithMany(x => x.Players)
                  .HasForeignKey(x => x.LeagueId);

            });

            builder.Entity<Manager>(gp =>
            {
                gp.HasKey(x => x.Id);

                gp.HasOne(x => x.Nation)
                  .WithMany(x => x.Managers)
                  .HasForeignKey(x => x.NationId);

                gp.HasOne(x => x.CurrentTeam)
                 .WithMany(x => x.Managers)
                 .HasForeignKey(x => x.CurrentTeamId);

                gp.HasMany(x => x.VirtualTeams)
                  .WithOne(x => x.Manager)
                 .OnDelete(DeleteBehavior.Restrict);

                gp.HasOne<IdentityUser>()
                  .WithOne()
                  .HasForeignKey<Manager>(x => x.UserId);

            });

            builder.Entity<VirtualTeam>(vt =>
            {
                vt.HasKey(x => x.Id);

                vt.HasOne(x => x.Team)
                  .WithMany(x => x.VirtualTeams)
                  .HasForeignKey(x => x.TeamId);

                vt.HasOne(x => x.Manager)
                  .WithMany(x => x.VirtualTeams)
                  .HasForeignKey(x => x.TeamId);

            });


            base.OnModelCreating(builder);
        }
    }
}