namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class TransferController : Controller
    {
        private readonly ServiceAggregator serviceAggregator;
        public TransferController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public async Task<IActionResult> Market()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var allFreeAgents = await serviceAggregator.transferService.GetAllFreeAgents(CurrentGame.Id, 0, CurrentGame, 0);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);

            return View(new TransferViewModel
            {
                FreeAgents = allFreeAgents,
                CurrentTeamPlayers = await serviceAggregator.transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = serviceAggregator.gameService.GetAllNations(),
                Positions = serviceAggregator.playerDataService.GetAllPositions(),
                CurrentTeam = currentTeam,
                PlayerAttributes = serviceAggregator.attributeService.GetAllPlayerAttributes(),
                PositionOrder = allFreeAgents.First().Position.Order,
                MenuViewModel = menuViewModel
            });
        }
        public async Task<IActionResult> SortPlayers(string text, int id, int positionOrder)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allFreeAgents = await serviceAggregator.transferService.GetAllFreeAgents(CurrentGame.Id, id, CurrentGame, positionOrder);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);

            return View("Market", new TransferViewModel
            {
                FreeAgents = allFreeAgents,
                CurrentTeamPlayers = await serviceAggregator.transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = serviceAggregator.gameService.GetAllNations(),
                Positions = serviceAggregator.playerDataService.GetAllPositions(),
                PlayerAttributes = serviceAggregator.attributeService.GetAllPlayerAttributes(),
                CurrentTeam = currentTeam,
                PositionOrder = positionOrder,
                MenuViewModel = menuViewModel
            });
        }
        public async Task<IActionResult> Buy(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isValid = serviceAggregator.validationService.BuyValidator(id, currentTeam);
            var currPl = await serviceAggregator.playerDataService.GetPlayerById(id);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);

            if (isValid)
            {
                serviceAggregator.transferService.Buy(id, currentTeam);
                serviceAggregator.inboxService.BuyPlayerNews(currPl, CurrentGame);
                serviceAggregator.teamService.CalculateTeamOverall(currentTeam);
                return RedirectToAction("Market");
            }
            else
            {
                ViewData["error"] = "Not enough money!";
                return View("ConfirmationTransfer", new TransferViewModel
                {
                    CurrentTeam = currentTeam,
                    PlayerToBuy = currPl,
                    Nations = serviceAggregator.gameService.GetAllNations(),
                    Positions = serviceAggregator.playerDataService.GetAllPositions(),
                    MenuViewModel = menuViewModel
                });
            }
        }
        public async Task<IActionResult> ConfirmationTransfer(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPl = await serviceAggregator.playerDataService.GetPlayerById(id);
            var menuViewModel = serviceAggregator.modelService.GetMenuViewModel(CurrentGame);

            return View(new TransferViewModel
            {
                CurrentTeam = currentTeam,
                PlayerToBuy = currPl,
                Nations = serviceAggregator.gameService.GetAllNations(),
                Positions = serviceAggregator.playerDataService.GetAllPositions(),
                MenuViewModel = menuViewModel
            });
        }
        public async Task<IActionResult> Sell(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool isValid = serviceAggregator.validationService.SellValidator(currentTeam);
            var currPlayers = await serviceAggregator.playerDataService.GetPlayersByTeam(currentTeam.Id);

            if (isValid)
            {
                serviceAggregator.transferService.Sell(id);
                serviceAggregator.inboxService.SellPlayerNews(id, CurrentGame);
            }
            else
            {
                ViewData["error"] = "You cannot be with less than 11 players!";
            }

            var model = serviceAggregator.modelService.GetTeamViewModel(currentTeam);
            return View("TeamSquad", model);
        }
        public async Task<IActionResult> TeamSquad()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.gameService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPlayers = await serviceAggregator.playerDataService.GetPlayersByTeam(currentTeam.Id);
            var model = serviceAggregator.modelService.GetTeamViewModel(currentTeam);

            return View(model);
        }

    }

}
