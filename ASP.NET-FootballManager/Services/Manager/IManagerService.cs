using ASP.NET_FootballManager.Models;

namespace ASP.NET_FootballManager.Services.Manager
{
    public interface IManagerService
    {


        void CreateNewManager(NewManagerViewModel ngvm,string userId);


    }
}
