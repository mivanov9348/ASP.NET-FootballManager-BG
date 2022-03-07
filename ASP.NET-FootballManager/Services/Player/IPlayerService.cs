namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IPlayerService
    {
        PlayersViewModel SortingPlayers(int sortBy);
        List<Player> GetPlayersByTeam(int teamId);
        void GeneratePlayers(Game game, VirtualTeam team);
        void CreateFreeAgents(Game game, int gk, int df, int mf, int st);
        void CalculatingPlayersPrice();


    }
}
