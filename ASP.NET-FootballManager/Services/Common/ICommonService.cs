namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data.DataModels;
    public interface ICommonService
    {

        List<Nation> GetAllNations();
        List<Team> GetAllTeams();

        Manager GetCurrentManager(string userId);



    }
}
