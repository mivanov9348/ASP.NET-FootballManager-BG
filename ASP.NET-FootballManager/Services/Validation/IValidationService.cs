using ASP.NET_FootballManager.Data.DataModels;
using ASP.NET_FootballManager.Models;

namespace ASP.NET_FootballManager.Services.Validation
{
    public interface IValidationService
    {
        (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm);
        bool BuyValidator(int id, VirtualTeam currentTeam);
        bool SellValidator(VirtualTeam currentTeam);

    }
}
