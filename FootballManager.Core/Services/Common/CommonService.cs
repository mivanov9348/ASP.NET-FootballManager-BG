namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Internal;

    public class CommonService : ICommonService
    {
        private readonly FootballManagerDbContext data;

        public CommonService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo(string userId)
        {
            var currentManager = this.data.Managers.FirstOrDefault(x => x.UserId == userId);
            var currentGame = this.data.Games.FirstOrDefault(x => x.ManagerId == currentManager.Id);
            var currentTeam = this.data.VirtualTeams.FirstOrDefault(x => x.GameId == currentGame.Id);
            return (userId, currentManager, currentGame, currentTeam);
        }
        public async Task<List<City>> GetAllCities() => await Task.Run(() => this.data.Cities.ToList());
        public async Task<List<Nation>> GetAllNations() => await Task.Run(() => this.data.Nations.ToList());
        public async Task<List<Player>> GetAllPlayers() => await Task.Run(() => this.data.Players.ToList());
        public async Task<List<Position>> GetAllPositions() => await Task.Run(() => this.data.Positions.ToList());
        public async Task<List<PlayerAttribute>> GetAllPlayersAttribute() => await Task.Run(() => this.data.PlayerAttributes.ToList());
        public async Task<List<PlayerStats>> GetAllPlayersStats() => await Task.Run(() => this.data.PlayerStats.ToList());

    }
}
