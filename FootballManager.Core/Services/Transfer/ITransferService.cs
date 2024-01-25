namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Models;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;

    public interface ITransferService
    {
        Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game CurrentGame, int positionOrder);
        Task<List<Player>> GetCurrentTeamPlayers(int teamId);
        void Buy(int playerId, VirtualTeam currentTeam);
        string Sell(int playerId);
    }
}
