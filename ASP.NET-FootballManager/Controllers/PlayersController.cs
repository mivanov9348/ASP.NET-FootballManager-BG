namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class PlayersController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public PlayersController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public IActionResult PlayersStats(PlayersViewModel pvm, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            pvm = serviceAggregator.playerSorterService.SortingPlayers(pvm.PlayerSorting, id, CurrentGame,1);
            var menuModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);
            pvm.MenuViewModel = menuModel;
            return View(pvm);
        }

        public async Task<IActionResult> PlayerDetails(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentPlayer = await serviceAggregator.playerDataService.GetPlayerById(id);
            var model = this.serviceAggregator.modelService.GetPlayerDetailsViewModel(currentPlayer, CurrentGame);

            return View(model);
        }
        public IActionResult Page( PlayersViewModel pvm, int pageNum)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            pvm = serviceAggregator.playerSorterService.SortingPlayers(pvm.PlayerSorting, 1, CurrentGame, pageNum);
            var menuModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);
            pvm.MenuViewModel = menuModel;
            return View("PlayersStats",pvm);
        }

    }
}
