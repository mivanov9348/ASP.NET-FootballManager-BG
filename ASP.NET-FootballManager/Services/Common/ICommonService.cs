namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        List<Nation> GetAllNations();
        List<Team> GetAllTeams();
        Manager GetCurrentManager(string userId);
        Game GetCurrentGame(int id);
        List<Game> GetAllUsersSaves(int managerId);
        List<Manager> GetAllManagers();
        List<Inbox> GetInboxMessages(int managerId);
        bool isExistGame(string UserId);
        void DeleteCurrentManager(string UserId);
    }
}
