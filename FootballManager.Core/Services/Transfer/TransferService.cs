namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
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
        public async Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game Game, int positionOrder)
        {
            var currentPlayers = this.data.Players.ToList();
            switch (orderId)
            {
                case 0:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.GameId == Game.Id).ToList());
                case 1:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == DataConstants.PositionOrder.Goalkeeper && x.GameId == Game.Id).ToList());
                case 2:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == DataConstants.PositionOrder.Defender && x.GameId == Game.Id).ToList());
                case 3:                        
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == DataConstants.PositionOrder.Midlefielder && x.GameId == Game.Id).ToList());
                case 4:                         
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == DataConstants.PositionOrder.Forward && x.GameId == Game.Id).ToList());
                case 5:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
                case 6:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.Nation.Name).ToList());                
                case 7:                   
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.Age).ToList());
             //   case 8:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.one).ThenBy(x => x.LastName).ToList());
             //   case 9:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 10:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 11:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 12:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 13:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 15:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 16:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 17:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 18:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
             //   case 19:
             //       return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
                case 20:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.GameId == Game.Id).OrderByDescending(x => x.Overall).ToList());
                case 21:
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
