namespace FootballManager.Test
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlayerTests : IDisposable
    {

        private SqliteConnection connection;
        private DbContextOptions<FootballManagerDbContext> options;
        private ServiceProvider serviceProvider;

      
        public VirtualTeam NewTeam(Game currentgame)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var team = new VirtualTeam
                {
                    TeamId = 1,
                    GameId = currentgame.Id
                };
                context.VirtualTeams.Add(team);
                context.SaveChanges();
                return team;
            }
        }
        public Game NewGame(int id)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var Game = new Game
                {
                    TeamId = 1
                };
                context.Games.Add(Game);
                context.SaveChanges();
                return Game;
            }
        }
        public Player NewPlayer(VirtualTeam team, Game currentGame, string firstName, string lastName)
        {
            using (var context = new FootballManagerDbContext(options))
            {
                var newPlayer = new Player
                {
                    FirstName = firstName,
                    LastName = lastName,
                    TeamId = team.Id,
                    IsStarting11 = true,
                    Age = 20,
                    LeagueId = 1,
                    CityId = 1,
                    GameId = currentGame.Id,
                    NationId = 1
                };
                context.Players.Add(newPlayer);
                context.SaveChanges();
                return newPlayer;
            }
        }
        public void Dispose()
        {
            connection.Close();
        }
    }
}
