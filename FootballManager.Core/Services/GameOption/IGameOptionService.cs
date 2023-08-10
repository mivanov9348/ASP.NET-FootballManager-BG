using FootballManager.Core.Models.GameOptions;
using System.Security.Claims;

namespace FootballManager.Core.Services.GameOption
{
    public interface IGameOptionService
    {
        bool IsOptionsSet(string userId);
        void SaveSampleOptions(string userId);
        void SaveOptions(ClaimsPrincipal user, GameOptionsViewModel model);
    }
}
