namespace FootballManager.Core.Services.Player.PlayerData
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public interface IPlayerDataService
    {
        Task<Player> GetRandomPlayer(VirtualTeam team);
        Task<Player> GetPlayerById(int id);
        Task<List<Player>> GetPlayersByTeam(int teamId);
        Task<Player> GetLeagueGoalscorer(Game CurrentGame, int leagueId);
        Task<List<Player>> GetStartingEleven(int teamId);
        Task<List<Player>> GetSubstitutes(int teamId);

    }
}
