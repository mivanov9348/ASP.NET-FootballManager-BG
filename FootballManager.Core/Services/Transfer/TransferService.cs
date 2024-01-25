    namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
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
            var budget = currentTeam.Budget - currentPlayer.Price;

            currentTeam.Budget = Math.Round(budget, 2);
            currentPlayer.Team = currentTeam;
            currentPlayer.FreeAgent = false;
            currentPlayer.TeamId = currentTeam.Id;

            this.data.SaveChanges();
        }
        public async Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game Game, int positionOrder)
        {
            var currentPlayers = this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).ToList();
            switch (orderId)
            {
                case 0:
                    return await Task.Run(() => currentPlayers);
                case 1:
                    return await Task.Run(() => currentPlayers.Where(x => x.PositionId == DataConstants.PositionOrder.Goalkeeper).ToList());
                case 2:
                    return await Task.Run(() => currentPlayers.Where(x => x.PositionId == DataConstants.PositionOrder.Defender).ToList());
                case 3:
                    return await Task.Run(() => currentPlayers.Where(x => x.PositionId == DataConstants.PositionOrder.Midlefielder).ToList());
                case 4:
                    return await Task.Run(() => currentPlayers.Where(x => x.PositionId == DataConstants.PositionOrder.Forward).ToList());
                case 5:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Id == positionOrder && x.GameId == Game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList());
                case 6:
                    return await Task.Run(() => currentPlayers.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderBy(x => x.Nation.Name).ToList());
                case 7:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.Age).ToList());
                case 8:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.PositionId == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.OneOnOne).ThenBy(x => x.FirstName).ToList());
                case 9:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Reflexes).ThenBy(x => x.FirstName).ToList());
                case 10:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Finishing).ThenBy(x => x.FirstName).ToList());
                case 11:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Passing).ThenBy(x => x.FirstName).ToList());
                case 12:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Heading).ThenBy(x => x.FirstName).ToList());
                case 13:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Tackling).ThenBy(x => x.FirstName).ToList());
                case 14:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Positioning).ThenBy(x => x.FirstName).ToList());
                case 15:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Pace).ThenBy(x => x.FirstName).ToList());
                case 16:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Stamina).ThenBy(x => x.FirstName).ToList());
                case 17:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Strength).ThenBy(x => x.FirstName).ToList());
                case 18:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.Dribbling).ThenBy(x => x.FirstName).ToList());
                case 19:
                    return await Task.Run(() => this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true && x.Position.Order == positionOrder && x.GameId == Game.Id).OrderByDescending(x => x.PlayerAttributes.BallControll).ThenBy(x => x.FirstName).ToList());
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
            var budget = currTeam.Budget + currPl.Price;

            currPl.FreeAgent = true;
            currPl.Team = freeAgentsTeam;
            currPl.TeamId = freeAgentsTeam.Id;
            currTeam.Budget = Math.Round(budget, 2);
            this.data.SaveChanges();

            return $"{currPl.FirstName} {currPl.LastName} sold!";
        }
    }
}
