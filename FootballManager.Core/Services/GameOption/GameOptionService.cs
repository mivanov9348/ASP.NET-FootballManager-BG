namespace FootballManager.Core.Services.GameOption
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.GameOptions;
    using Infrastructure.Data.DataModels;
    using System.Security.Claims;
    public class GameOptionService : IGameOptionService
    {
        private readonly FootballManagerDbContext data;

        public GameOptionService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public void SaveOptions(ClaimsPrincipal user, GameOptionsViewModel model)
        {
            var userId = "";

            if (user.Identity.IsAuthenticated != false)
            {
                userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            var currentUserGameOptions = this.data.GameOptions.FirstOrDefault(x => x.UserId == userId);

            if (currentUserGameOptions != null)
            {
                currentUserGameOptions.TimeInterval = model.TimeInterval;
                currentUserGameOptions.WinCoins = model.WinCoins;
                currentUserGameOptions.DrawCoins = model.DrawCoins;
                currentUserGameOptions.FirstPlaceCoins = model.FirstPlaceCoins;
                currentUserGameOptions.SecondPlaceCoins = model.SecondPlaceCoins;
                currentUserGameOptions.ThirdPlaceCoins = model.ThirdPlaceCoins;
                currentUserGameOptions.StartingCoins = model.StartingCoins;
                currentUserGameOptions.PlayerMaximumAge = model.PlayerMaximumAge;
                currentUserGameOptions.PlayerMinimumAge = model.PlayerMinimumAge;
            }
            else
            {
                var newGameOptions = new GameOption
                {
                    UserId = userId,
                    TimeInterval = model.TimeInterval,
                    WinCoins = model.WinCoins,
                    DrawCoins = model.DrawCoins,
                    FirstPlaceCoins = model.FirstPlaceCoins,
                    SecondPlaceCoins = model.SecondPlaceCoins,
                    ThirdPlaceCoins = model.ThirdPlaceCoins,
                    StartingCoins = model.StartingCoins,
                    PlayerMaximumAge = model.PlayerMaximumAge,
                    PlayerMinimumAge = model.PlayerMinimumAge
                };
                this.data.GameOptions.Add(newGameOptions);
            }
            this.data.SaveChanges();
        }
    }
}
