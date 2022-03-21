namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
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
        public List<Player> GetAllFreeAgents(int gameId, int orderId)
        {
            switch (orderId)
            {
                case 0:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).ToList();
                case 1:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Goalkeeper").ToList();
                case 2:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Defender").ToList();
                case 3:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Midlefielder").ToList();
                case 4:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Name == "Striker").ToList();
                case 5:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).OrderByDescending(x => x.Attack).ToList();
                case 6:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).OrderByDescending(x => x.Defense).ToList();
                case 7:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).OrderByDescending(x => x.Overall).ToList();
                case 8:
                    return this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).OrderByDescending(x => x.Price).ToList();
            }
            return null;
        }
        public List<Player> GetCurrentTeamPlayers(int teamId) => this.data.Players.Where(x => x.TeamId == teamId).ToList();

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
