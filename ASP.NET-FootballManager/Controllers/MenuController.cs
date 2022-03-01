namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;

    public class MenuController : Controller
    {
        private readonly ICommonService commonService;
        private readonly IManagerService managerService;
        private readonly ILeagueService leagueService;
        private readonly IGameService gameService;
        public MenuController(IGameService gameService, ICommonService commonService, ILeagueService leagueService, IManagerService managerService)
        {
            this.commonService = commonService;
            this.leagueService = leagueService;
            this.managerService = managerService;
            this.gameService = gameService;
        }

        public IActionResult Inbox(int id)
        {
            (string UserId, Manager currentManager, Game CurrentGame) = CurrentGameInfo();
            var currentInboxMessage = commonService.GetInboxMessages(CurrentGame.Id);

            return View(new InboxViewModel
            {
                News = currentInboxMessage.OrderByDescending(x => x.Id).ToList()
            });
        }
        public IActionResult NextMatch()
        {
            return View();
        }
        public IActionResult Standings()
        {
            (string UserId, Manager currentManager, Game CurrentGame) = CurrentGameInfo();
            var teams = leagueService.CurrentGameTeams(CurrentGame);

            return View(new StandingsViewModel
            {
                VirtualTeams = teams
            });
        }
        public IActionResult Fixtures()
        {
            return View();
        }
        private (string UserId, Manager currentManager, Game CurrentGame) CurrentGameInfo()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentManager = managerService.GetCurrentManager(userId);
            var currentGame = gameService.GetCurrentGame(currentManager.Id);
            return (userId, currentManager, currentGame);
        }

    }
}
