namespace ASP.NET_FootballManager.Services.Validation
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    public interface IValidationService
    {
        (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm);
        bool BuyValidator(int id, VirtualTeam currentTeam);
        bool SellValidator(VirtualTeam currentTeam);

    }
}
