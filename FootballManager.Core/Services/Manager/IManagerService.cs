namespace ASP.NET_FootballManager.Services.Manager
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    public interface IManagerService
    {


        Manager CreateNewManager(NewManagerViewModel ngvm,string userId);
        Manager GetCurrentManager(string userId);
        List<Manager> GetAllManagers();
        void DeleteCurrentManager(string UserId);

    }
}
