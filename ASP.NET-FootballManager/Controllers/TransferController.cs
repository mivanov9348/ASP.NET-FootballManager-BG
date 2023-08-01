namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using ASP.NET_FootballManager.Services.Transfer;
    using ASP.NET_FootballManager.Services.Validation;
    using FootballManager.Core.Models.Player;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class TransferController : Controller
    {
        private readonly ITransferService transferService;
        private readonly ICommonService commonService;
        private readonly IValidationService validatorService;
        private readonly ITeamService teamService;
        private readonly IPlayerService playerService;
        private readonly IInboxService inboxService;
        public TransferController(IInboxService inboxService, IPlayerService playerService, ITeamService teamService, IValidationService validatorService, ITransferService transferService, ICommonService commonService)
        {
            this.transferService = transferService;
            this.commonService = commonService;
            this.validatorService = validatorService;
            this.teamService = teamService;
            this.playerService = playerService;
            this.inboxService = inboxService;
        }
        public async Task<IActionResult> Market()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(new TransferViewModel
            {
                FreeAgents = await transferService.GetAllFreeAgents(CurrentGame.Id, 0, CurrentGame),
                CurrentTeamPlayers = await transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = await commonService.GetAllNations(),
                Positions = await commonService.GetAllPositions(),
                CurrentTeam = currentTeam,
                PlayerAttributes = await commonService.GetAllPlayersAttribute()
            });
        }
        public async Task<IActionResult> SortPlayers(string text, int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return View("Market", new TransferViewModel
            {
                FreeAgents = await transferService.GetAllFreeAgents(CurrentGame.Id, id, CurrentGame),
                CurrentTeamPlayers = await transferService.GetCurrentTeamPlayers(currentTeam.Id),
                Nations = await commonService.GetAllNations(),
                Positions = await commonService.GetAllPositions(),
                CurrentTeam = currentTeam
            });
        }
        public async Task<IActionResult> Buy(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            bool isValid = validatorService.BuyValidator(id, currentTeam);
            var currPl = await playerService.GetPlayerById(id);

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
                    Nations = await commonService.GetAllNations(),
                    Positions = await commonService.GetAllPositions()
                });
            }
        }
        public async Task<IActionResult> ConfirmationTransfer(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPl = await playerService.GetPlayerById(id);
            return View(new TransferViewModel
            {
                CurrentTeam = currentTeam,
                PlayerToBuy = currPl,
                Nations = await commonService.GetAllNations(),
                Positions = await commonService.GetAllPositions()
            });
        }
        public async Task<IActionResult> Sell(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            bool isValid = validatorService.SellValidator(currentTeam);
            var currPlayers = await playerService.GetPlayersByTeam(currentTeam.Id);

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
        public async Task<IActionResult> TeamSquad()
        {
            (string UserId, Manager currentManager, Game CurrentGame, VirtualTeam currentTeam) = commonService.CurrentGameInfo(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currPlayers = await playerService.GetPlayersByTeam(currentTeam.Id);
            var model = teamService.GetTeamViewModel(currPlayers.OrderBy(x => x.PositionId).ToList(), currentTeam);

            return View(model);
        }

    }

}
