﻿namespace ASP.NET_FootballManager.Services.Game
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Database.ImportDto;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Team;
    using Newtonsoft.Json;

    public class GameService : IGameService
    {
        private readonly FootballManagerDbContext data;

        private Random rnd;
        public GameService(FootballManagerDbContext data)
        {
            this.data = data;
            rnd = new Random();
        }
        public bool isExistGame(string UserId)
        {
            var currManager = this.data.Managers.FirstOrDefault(x => x.UserId == UserId);
            if (currManager != null)
            {
                return true;
            }
            return false;
        }
        public Game GetCurrentGame(int id) => this.data.Games.FirstOrDefault(x => x.ManagerId == id);
        public Game CreateNewGame(Manager manager)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == manager.CurrentTeamId);

            var newGame = new Game
            {
                Manager = manager,
                Season = 1,
                Year = 1,
                CupRound = 1,
                EuroCupRound = 1,
                LeagueRound = 1,
                Day = 1,
                ManagerId = manager.Id,
                Team = currentTeam,
                TeamId = currentTeam.Id
            };

            this.data.Games.Add(newGame);
            this.data.SaveChanges();
            return newGame;
        }
        public void NextDay(Game currentGame)
        {
            currentGame.Day += 1;
            this.data.SaveChanges();
        }
        public void ResetGame(Game CurrentGame)
        {
            CurrentGame.Day = 1;
            CurrentGame.Year += 1;
            CurrentGame.Season += 1;
            CurrentGame.CupRound = 1;
            CurrentGame.LeagueRound = 1;
            CurrentGame.EuroCupRound = 1;
            this.data.SaveChanges();
        }
        public void ResetSave(string UserId)
        {
            var userManager = this.data.Managers.FirstOrDefault(x => x.UserId == UserId);
            var userGame = this.data.Games.FirstOrDefault(x => x.ManagerId == userManager.Id);

            var matches = this.data.Matches.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, matches.Count());
            var players = this.data.Players.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, players.Count());
            var fixtures = this.data.Fixtures.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, fixtures.Count());
            var inboxes = this.data.Inboxes.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, inboxes.Count());
            var virtualTeams = this.data.VirtualTeams.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, virtualTeams.Count());
            var days = this.data.Days.Where(x => x.GameId == userGame.Id).ToList();
            matches.RemoveRange(0, days.Count());

            this.data.Games.Remove(userGame);
            this.data.Managers.Remove(userManager);

            this.data.SaveChanges();
        }
    }
}
