namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {
        List<Nation> GetAllNations();
        List<Team> GetAllTeams();
        List<Game> GetAllUsersSaves(int managerId);
        List<Inbox> GetInboxMessages(int managerId);
         
           
    }
}
