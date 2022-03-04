namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        List<Nation> GetAllNations();
        List<VirtualTeam> GetAllVirtualTeams();
        List<Team> GetAllTeams();
        List<Position> GetAllPositions();
        List<City> GetAllCities();
        List<Player> GetAllPlayers();
        List<Fixture> GetFixture(int id);
        List<Game> GetAllUsersSaves(int managerId);
        List<Inbox> GetInboxMessages(int managerId);
        VirtualTeam GetCurrentTeam(Game currentGame);
        Team GetOriginalTeam(VirtualTeam currentVirtual);
        void DeleteNews(int id);
    }
}
