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
        public async Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game game, int positionOrder)
        {
            var freeAgents = this.data.Players.Where(x => x.GameId == gameId && x.FreeAgent == true).ToList();

            if (orderId == 0)
                return await Task.Run(() => freeAgents);

            var positionFilters = new Dictionary<int, int>
    {
        { 1, DataConstants.PositionOrder.Goalkeeper },
        { 2, DataConstants.PositionOrder.Defender },
        { 3, DataConstants.PositionOrder.Midlefielder },
        { 4, DataConstants.PositionOrder.Forward }
    };

            if (positionFilters.ContainsKey(orderId))
                return await Task.Run(() => freeAgents.Where(x => x.PositionId == positionFilters[orderId]).ToList());

            var orderedResults = orderId switch
            {
                5 => freeAgents.Where(x => x.Position.Id == positionOrder && x.GameId == game.Id).OrderBy(x => x.FirstName).ThenBy(x => x.LastName),
                6 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderBy(x => x.Nation.Name),
                7 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.Age),
                8 => freeAgents.Where(x => x.PositionId == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.OneOnOne).ThenBy(x => x.FirstName),
                9 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Reflexes).ThenBy(x => x.FirstName),
                10 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Finishing).ThenBy(x => x.FirstName),
                11 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Passing).ThenBy(x => x.FirstName),
                12 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Heading).ThenBy(x => x.FirstName),
                13 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Tackling).ThenBy(x => x.FirstName),
                14 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Positioning).ThenBy(x => x.FirstName),
                15 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Pace).ThenBy(x => x.FirstName),
                16 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Stamina).ThenBy(x => x.FirstName),
                17 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Strength).ThenBy(x => x.FirstName),
                18 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.Dribbling).ThenBy(x => x.FirstName),
                19 => freeAgents.Where(x => x.Position.Order == positionOrder && x.GameId == game.Id).OrderByDescending(x => x.PlayerAttributes.BallControll).ThenBy(x => x.FirstName),
                20 => freeAgents.Where(x => x.GameId == game.Id).OrderByDescending(x => x.Overall),
                21 => freeAgents.Where(x => x.GameId == game.Id).OrderByDescending(x => x.Price),
                _ => null
            };

            return orderedResults != null ? await Task.Run(() => orderedResults.ToList()) : null;
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
