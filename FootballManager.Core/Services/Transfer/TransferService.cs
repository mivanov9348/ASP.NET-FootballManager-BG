namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using System.Collections.Generic;

    public class TransferService : ITransferService
    {
        private readonly FootballManagerDbContext data;
        public TransferService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public void Buy(int playerId, VirtualTeam currentTeam)
        {
            var currentPlayer = this.data.Players.FirstOrDefault(x => x.Id == playerId);

            currentTeam.Budget -= currentPlayer.Price;
            currentPlayer.Team = currentTeam;
            currentPlayer.FreeAgent = false;
            currentPlayer.TeamId = currentTeam.Id;

            this.data.SaveChanges();
        }
        public async Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game Game)
        {
            switch (orderId)
            {
                case 0:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.GameId == Game.Id).ToList());
                case 1:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Goalkeeper" && x.GameId == Game.Id).ToList());
                case 2:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Defender" && x.GameId == Game.Id).ToList());
                case 3:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Midlefielder" && x.GameId == Game.Id).ToList());
                case 4:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Striker" && x.GameId == Game.Id).ToList());
                case 7:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.GameId == Game.Id).OrderByDescending(x => x.Overall).ToList());
                case 8:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.GameId == Game.Id).OrderByDescending(x => x.Price).ToList());
            }
            return null;
        }
        public async Task<List<Player>> GetCurrentTeamPlayers(int teamId) => await Task.Run(() => this.data.Players.Where(x => x.TeamId == teamId).ToList());
        public string Sell(int playerId)
        {
            var currPl = this.data.Players.FirstOrDefault(x => x.Id == playerId);
            var currTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currPl.TeamId);
            var freeAgentsTeam = this.data.VirtualTeams.FirstOrDefault(x => x.IsPlayable == false);

            currPl.FreeAgent = true;
            currPl.Team = freeAgentsTeam;
            currPl.TeamId = freeAgentsTeam.Id;
            currTeam.Budget += currPl.Price;
            this.data.SaveChanges();

            return $"{currPl.FirstName} {currPl.LastName} sold!";
        }
    }
}
