namespace ASP.NET_FootballManager.Services.Player
{
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Data.DataModels;
    public interface IPlayerService
    {

        PlayersViewModel SortingPlayers(int sortBy);

        List<Player> GetPlayersByTeam(int teamId);
    }
}
