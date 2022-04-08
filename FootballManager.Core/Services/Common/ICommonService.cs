namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        List<Nation> GetAllNations();    
        List<Position> GetAllPositions();
        List<City> GetAllCities();
        List<Player> GetAllPlayers();       

    }
}
