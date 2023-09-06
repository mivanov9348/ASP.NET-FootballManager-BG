namespace ASP.NET_FootballManager.Services.Manager
{
    using ASP.NET_FootballManager.Models;
    using FootballManager.Infrastructure.Data.DataModels;
    public interface IManagerService
    {
        Manager CreateNewManager(NewManagerViewModel ngvm,string userId);
        Manager GetCurrentManager(string userId);       
        void DeleteCurrentManager(string UserId);
        void AddImageToManager(NewManagerViewModel model, string userId);

    }
}
