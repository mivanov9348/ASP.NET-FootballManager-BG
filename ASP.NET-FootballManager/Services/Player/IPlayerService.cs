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
        List<Player> GetStartingEleven(int teamId);
        List<Player> GetSubstitutes(int teamId);
        void Substitution(int playerId, string action);
        Player GetPlayerById(int id);
        Player GetRandomPlayer(VirtualTeam team);
        Player GetGoalscorer(Game CurrentGame);
        void RemovePlayers(VirtualTeam freeAgentsTeam);
    }
}
