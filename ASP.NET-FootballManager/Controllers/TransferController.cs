namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Services;
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
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var allFreeAgents = await serviceAggregator.transferService.GetAllFreeAgents(CurrentGame.Id, 0, CurrentGame, 0);
            return View(new TransferViewModel
            {
                FreeAgents = allFreeAgents,
                CurrentTeamPlayers = await serviceAggregator.transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Positions = await serviceAggregator.commonService.GetAllPositions(),
                CurrentTeam = currentTeam,
                PlayerAttributes = await serviceAggregator.commonService.GetAllPlayersAttribute(),
                PositionOrder = allFreeAgents.First().Position.Order
            });
        }
        public async Task<IActionResult> SortPlayers(string text, int id, int positionOrder)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var allFreeAgents = await serviceAggregator.transferService.GetAllFreeAgents(CurrentGame.Id, id, CurrentGame, positionOrder);

            return View("Market", new TransferViewModel
            {
                FreeAgents = allFreeAgents,
                CurrentTeamPlayers = await serviceAggregator.transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Positions = await serviceAggregator.commonService.GetAllPositions(),
                PlayerAttributes = await serviceAggregator.commonService.GetAllPlayersAttribute(),
                CurrentTeam = currentTeam,
                PositionOrder = allFreeAgents.First().Position.Order
            });
        }
        public async Task<IActionResult> Buy(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isValid = serviceAggregator.validationService.BuyValidator(id, currentTeam);
            var currPl = await serviceAggregator.playerDataService.GetPlayerById(id);

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
                    Nations = await serviceAggregator.commonService.GetAllNations(),
                    Positions = await serviceAggregator.commonService.GetAllPositions()
                });
            }
        }
        public async Task<IActionResult> ConfirmationTransfer(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPl = await serviceAggregator.playerDataService.GetPlayerById(id);
            return View(new TransferViewModel
            {
                CurrentTeam = currentTeam,
                PlayerToBuy = currPl,
                Nations = await serviceAggregator.commonService.GetAllNations(),
                Positions = await serviceAggregator.commonService.GetAllPositions()
            });
        }
        public async Task<IActionResult> Sell(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

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

            var model = serviceAggregator.teamService.GetTeamViewModel(currPlayers, currentTeam);
            return View("TeamSquad", model);
        }
        public async Task<IActionResult> TeamSquad()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = serviceAggregator.commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPlayers = await serviceAggregator.playerDataService.GetPlayersByTeam(currentTeam.Id);
            var model = serviceAggregator.teamService.GetTeamViewModel(currPlayers.OrderBy(x => x.PositionId).ToList(), currentTeam);

            return View(model);
        }

    }

}
