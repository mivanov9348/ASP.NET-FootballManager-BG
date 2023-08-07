using FootballManager.Core.Models.GameOptions;
using System.Security.Claims;

namespace FootballManager.Core.Services.GameOption
{
    public interface IGameOptionService
    {      
        void SaveOptions(ClaimsPrincipal user, GameOptionsViewModel model);
    }
}
