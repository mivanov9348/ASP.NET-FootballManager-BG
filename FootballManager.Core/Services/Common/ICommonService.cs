namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;

    public interface ICommonService
    {
        Task<List<Nation>> GetAllNations();
        Task<List<Position>> GetAllPositions();
        Task<List<City>> GetAllCities();
        Task<List<Player>> GetAllPlayers();
        Task<List<PlayerAttribute>> GetAllPlayersAttribute();
        (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo(string userId);
    }
}
