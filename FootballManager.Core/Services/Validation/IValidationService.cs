namespace ASP.NET_FootballManager.Services.Validation
{
    using ASP.NET_FootballManager.Models;
    using FootballManager.Infrastructure.Data.DataModels;

    public interface IValidationService
    {
        (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm);
        bool BuyValidator(int id, VirtualTeam currentTeam);
        bool SellValidator(VirtualTeam currentTeam);

    }
}
