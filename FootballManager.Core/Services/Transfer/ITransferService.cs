namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;

    public interface ITransferService
    {
        Task<List<Player>> GetAllFreeAgents(int gameId, int orderId, Game CurrentGame, int positionOrder);
        Task<List<Player>> GetCurrentTeamPlayers(int teamId);
        TransferViewModel GetTransferViewModel(Player player);
        void Buy(int playerId, VirtualTeam currentTeam);
        string Sell(int playerId);
    }
}
