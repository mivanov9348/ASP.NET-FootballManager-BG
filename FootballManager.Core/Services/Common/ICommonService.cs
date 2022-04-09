namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        Task<List<Nation>> GetAllNations();
        Task<List<Position>> GetAllPositions();
        Task<List<City>> GetAllCities();
        Task<List<Player>> GetAllPlayers();

    }
}
