namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        List<Nation> GetAllNations();
        List<VirtualTeam> GetAllVirtualTeams(Game currentGame);
        List<Team> GetAllTeams();
        List<Position> GetAllPositions();
        List<City> GetAllCities();
        List<Player> GetAllPlayers();
        List<Fixture> GetFixture(int id, int round);
        List<Game> GetAllUsersSaves(int managerId);
        List<Inbox> GetInboxMessages(int managerId);
        VirtualTeam GetCurrentTeam(Game currentGame);
        Team GetOriginalTeam(VirtualTeam currentVirtual);
        void DeleteNews(int id);
        VirtualTeam GetTeamById(int teamId);
        int GetAllRounds(int leagueId);
    }
}
