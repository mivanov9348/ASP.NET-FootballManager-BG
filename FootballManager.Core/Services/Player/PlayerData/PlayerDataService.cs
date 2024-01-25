namespace FootballManager.Core.Services.Player.PlayerData
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;

    public class PlayerDataService : IPlayerDataService
    {
        private Random rnd;
        private readonly FootballManagerDbContext data;
        public PlayerDataService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
        }

        public async Task<Player> GetRandomPlayer(VirtualTeam team)
        {
            var players = await Task.Run(() => this.data.Players.Where(x => x.GameId == team.GameId && x.IsStarting11 == true && x.TeamId == team.Id && x.Position.Name != "Goalkeeper").ToList());
            return players[rnd.Next(0, players.Count)];
        }
        public async Task<Player> GetPlayerById(int id) => await Task.Run(() => this.data.Players.FirstOrDefault(x => x.Id == id));
        public async Task<List<Player>> GetPlayersByTeam(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.TeamId == teamId).ToList());
        public async Task<Player> GetLeagueGoalscorer(Game CurrentGame, int leagueId)
        {
            if (leagueId == 0)
            {
                leagueId = await Task.Run(() => this.data.Leagues.FirstOrDefault(x => x.Level == DataConstants.LeagueLevels.FirstLevel).Id);
            }

            var currentComp = this.data.Leagues.FirstOrDefault(x => x.Id == leagueId);
            var sortedPlayersByGoal = this.data.Players.Include(x => x.PlayerStats).OrderByDescending(x => x.PlayerStats.Goals).ThenByDescending(x => x.PlayerStats.Appearance);
            return await Task.Run(() => sortedPlayersByGoal.FirstOrDefault(x => x.GameId == CurrentGame.Id && x.LeagueId == leagueId));
        }
        public async Task<List<Player>> GetStartingEleven(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.IsStarting11 == true && x.TeamId == teamId).ToList());
        public async Task<List<Player>> GetSubstitutes(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.IsStarting11 == false && x.TeamId == teamId).ToList());
        public void RemovePlayers(VirtualTeam freeAgentsTeam)
        {
            var allPlayers = this.data.Players.Where(x => x.TeamId == freeAgentsTeam.Id).ToList();

            foreach (var item in allPlayers)
            {
                this.data.Players.Remove(item);
            }
            this.data.SaveChanges();
        }

        public List<Position> GetAllPositions() => this.data.Positions.ToList();
    }
}
