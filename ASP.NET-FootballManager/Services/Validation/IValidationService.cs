using ASP.NET_FootballManager.Models;

namespace ASP.NET_FootballManager.Services.Validation
{
    public interface IValidationService
    {

        (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm);



    }
}
