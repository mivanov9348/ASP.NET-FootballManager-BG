namespace ASP.NET_FootballManager.Services.Transfer
{
    using ASP.NET_FootballManager.Models;
    using Data.DataModels;
    public interface ITransferService
    {
        List<Player> GetAllFreeAgents(int gameId, int orderId,Game CurrentGame);
        List<Player> GetCurrentTeamPlayers(int teamId);
        void Buy(int playerId, VirtualTeam currentTeam);
        string Sell(int playerId);
    }
}
