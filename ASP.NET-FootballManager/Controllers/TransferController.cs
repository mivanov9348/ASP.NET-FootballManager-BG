namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using ASP.NET_FootballManager.Services.Transfer;
    using ASP.NET_FootballManager.Services.Validation;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class TransferController : Controller
    {
        private readonly ITransferService transferService;
        private readonly IGameService gameService;
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly IValidationService validatorService;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly IInboxService inboxService;
        public TransferController(IInboxService inboxService, IPlayerService playerService, ITeamService teamService, IValidationService validatorService, ITransferService transferService, IGameService gameService, ICommonService commonService, IManagerService managerService)
        {
            this.transferService = transferService;
            this.gameService = gameService;
            this.commonService = commonService;
            this.managerService = managerService;
            this.validatorService = validatorService;
            this.teamService = teamService;
            this.playerService = playerService;
            this.inboxService = inboxService;

        }
        public IActionResult Market()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            return View(new TransferViewModel
            {
                FreeAgents = transferService.GetAllFreeAgents(CurrentGame.Id, 0, CurrentGame),
                CurrentTeamPlayers = transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = commonService.GetAllNations(),
                Positions = commonService.GetAllPositions(),
                CurrentTeam = currentTeam
            });
        }
        public IActionResult SortPlayers(string text, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            return View("Market", new TransferViewModel
            {
                FreeAgents = transferService.GetAllFreeAgents(CurrentGame.Id, id, CurrentGame),
                CurrentTeamPlayers = transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = commonService.GetAllNations(),
                Positions = commonService.GetAllPositions(),
                CurrentTeam = currentTeam
            });
        }
        public IActionResult Buy(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            bool isValid = validatorService.BuyValidator(id, currentTeam);
            var currPl = playerService.GetPlayerById(id);

            if (isValid)
            {
                transferService.Buy(id, currentTeam);
                inboxService.BuyPlayerNews(currPl, CurrentGame);
                teamService.CalculateTeamOverall(currentTeam);
                return RedirectToAction("Market");
            }
            else
            {
                ViewData["error"] = "Not enough money!";
                return View("ConfirmationTransfer", new TransferViewModel
                {
                    CurrentTeam = currentTeam,
                    PlayerToBuy = currPl,
                    Nations = commonService.GetAllNations(),
                    Positions = commonService.GetAllPositions()
                });
            }
        }
        public IActionResult ConfirmationTransfer(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currPl = playerService.GetPlayerById(id);
            return View(new TransferViewModel
            {
                CurrentTeam = currentTeam,
                PlayerToBuy = currPl,
                Nations = commonService.GetAllNations(),
                Positions = commonService.GetAllPositions()
            });
        }
        public IActionResult Sell(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();

            bool isValid = validatorService.SellValidator(currentTeam);
            var currPlayers = playerService.GetPlayersByTeam(currentTeam.Id);

            if (isValid)
            {
                transferService.Sell(id);
                inboxService.SellPlayerNews(id, CurrentGame);
            }
            else
            {
                ViewData["error"] = "You cannot be with less than 11 players!";
            }

            var model = teamService.GetTeamViewModel(currPlayers, currentTeam);
            return View("TeamSquad", model);
        }
        public IActionResult TeamSquad()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = CurrentGameInfo();
            var currPlayers = playerService.GetPlayersByTeam(currentTeam.Id);
            var model = teamService.GetTeamViewModel(currPlayers, currentTeam);

            return View(model);
        }
        private (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) CurrentGameInfo()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentManager = managerService.GetCurrentManager(userId);
            var currentGame = gameService.GetCurrentGame(currentManager.Id);
            var currentTeam = teamService.GetCurrentTeam(currentGame);
            return (userId, currentManager, currentGame, currentTeam);
        }
    }

}
