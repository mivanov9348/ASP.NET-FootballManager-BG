﻿namespace ASP.NET_FootballManager.Data
{  
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class FootballManagerDbContext : IdentityDbContext
    {
        public DbSet<Nation> Nations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerAttribute> PlayerAttributes { get; set; }
        public DbSet<PlayerStats> PlayerStats { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<VirtualTeam> VirtualTeams { get; set; }
        public DbSet<Inbox> Inboxes { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<ContinentalCup> ContinentalCups { get; set; }
        public DbSet<Cup> Cups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GameOption> GameOptions { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public FootballManagerDbContext(DbContextOptions<FootballManagerDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}