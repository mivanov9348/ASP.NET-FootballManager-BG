﻿namespace ASP.NET_FootballManager.Services.Team
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;

    public class TeamService : ITeamService
    {
        private readonly FootballManagerDbContext data;
        public TeamService(FootballManagerDbContext data)
        {
            this.data = data;
        }
        public List<VirtualTeam> GenerateTeams(Game game)
        {
            var allTeam = this.data.Teams.ToList();

            var virtualTeams = allTeam.Select(x => new VirtualTeam
            {
                Team = x,
                TeamId = x.Id,
                Name = x.Name,
                Game = game,
                GameId = game.Id,
                LeagueId = x.LeagueId,
                IsPlayable = x.IsPlayable

            }).ToList();

            foreach (var team in virtualTeams)
            {
                this.data.VirtualTeams.Add(team);
            }
            this.data.SaveChanges();
            return virtualTeams;
        }
        public List<VirtualTeam> CurrentGameTeams(Game currentGame) => this.data.VirtualTeams.ToList();
        public void CalculateTeamOverall(VirtualTeam team)
        {
            team.Overall = 0;
            var teamPlayers = this.data.Players.Where(x => x.TeamId == team.Id).ToList();
            var overallSum = teamPlayers.Sum(x => x.Overall);
            team.Overall = overallSum / teamPlayers.Count;

            this.data.SaveChanges();
        }
        public TeamViewModel GetTeamViewModel(List<Player> currPlayers, VirtualTeam currentTeam)
        {
            return new TeamViewModel
            {
                Positions = this.data.Positions.ToList(),
                Cities = this.data.Cities.ToList(),
                Players = currPlayers,
                CurrentTeam = currentTeam,
                Nations = this.data.Nations.ToList()
            };
        }

        public void ResetTeams(Game currentGame)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id);

            foreach (var team in allTeams)
            {
                team.Matches = 0;
                team.Wins = 0;
                team.Draws = 0;
                team.Loses = 0;
                team.GoalScored = 0;
                team.GoalAgainst = 0;
                team.GoalDifference = 0;
                team.Points = 0;
            }
            this.data.SaveChanges();
        }
    }
}
